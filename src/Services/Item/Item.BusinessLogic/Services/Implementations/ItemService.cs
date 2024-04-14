using AutoMapper;
using Item.BusinessLogic.Exceptions;
using Item.BusinessLogic.Exceptions.ErrorMessages;
using Item.BusinessLogic.Models.Common;
using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Models.DTOs.Filter;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Data;
using Item.DataAccess.Data.Initializers.Values;
using Item.DataAccess.Enums;
using Item.DataAccess.Models;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.Specifications.Implementations.Item;
using Item.DataAccess.Transactions.Interfaces;
using System.Linq.Expressions;

namespace Item.BusinessLogic.Services.Implementations;

using Item = DataAccess.Models.Item;

public class ItemService(
    IRepository<Item> _itemRepository, 
    IRepository<Status> _statusRepository,
    ICurrentUserService _currentUserService,
    IItemImageService _imageService,
    ITransactionManager _transactionManager,
    IMapper _mapper) 
    : IItemService
{
    public async Task<Item> CreateAsync(ItemDto itemDto, CancellationToken token)
    {
        var item = _mapper.Map<ItemDto, Item>(itemDto);

        item.Status = await _statusRepository.SingleOrDefaultAsync(x => x.NormalizedName == StatusValues.UnderReview.NormalizedName, token);

        item.UserId = _currentUserService.UserId!.Value;

        var createdItem = await _itemRepository.AddAsync(item, token);

        try
        {
            await _imageService.SaveAttachedImagesAsync(createdItem.Id, itemDto.AttachedImages, token);
        }
        catch
        {
            await DeleteByIdAsync(createdItem.Id, token);

            throw;
        }

        return createdItem;
    }

    public async Task<Item> UpdateAsync(Guid id, ItemDto itemDto, CancellationToken token)
    {
        var item = await _itemRepository.GetByIdAsync(id, token);

        NotFoundException.ThrowIfNull(item);

        var currentUserId = _currentUserService.UserId;

        var currentRole = _currentUserService.Role;

        if (!(currentUserId == item.UserId || 
            currentRole == nameof(Role.Administrator) || 
            currentRole == nameof(Role.Moderator)))
        {
            throw new ForbiddenException();
        }

        using var transaction = await _transactionManager.BeginTransactionAsync(token);

        _mapper.Map(itemDto, item);

        item.Status = await _statusRepository.SingleOrDefaultAsync(x => x.Equals(StatusValues.UnderReview), token);

        var updatedImage = await _itemRepository.UpdateAsync(item, token);

        var oldImages = await _imageService.GetItemImagesAsync(item.Id, token);

        try
        {
            await _imageService.SaveAttachedImagesAsync(item.Id, itemDto.AttachedImages, token);

            await _imageService.DeleteAttachedImagesAsync(oldImages, token);

            await transaction.CommitAsync(token);

            return updatedImage;
        }
        catch
        {
            var allImages = await _imageService.GetItemImagesAsync(item.Id, token);
            var imagesToDelete = allImages.ExceptBy(oldImages.Select(x => x.Id), x => x.Id);

            await _imageService.DeleteAttachedImagesAsync(imagesToDelete, token);

            await transaction.RollbackAsync(token);

            throw;
        }
    }

    public async Task<Item> DeleteByIdAsync(Guid id, CancellationToken token)
    {
        var item = await _itemRepository.GetByIdAsync(id, token);

        NotFoundException.ThrowIfNull(item);

        var currentUserId = _currentUserService.UserId;

        var currentRole = _currentUserService.Role;

        if (currentUserId == item.UserId ||
            currentRole == nameof(Role.Administrator) ||
            currentRole == nameof(Role.Moderator))
        {
            await _imageService.DeleteAllAttachedImagesAsync(item.Id, token);

            await _itemRepository.DeleteAsync(item, token);

            return item;
        }
        else
        {
            throw new ForbiddenException();
        }
    }

    public async Task<Item> ChangeStatus(Guid id, UpdateStatusDto updateStatusDto, CancellationToken token = default)
    {
        var specification = new ItemWithStatusSpecification(id);
        var item = await _itemRepository.FirstOrDefaultAsync(specification, token);

        NotFoundException.ThrowIfNull(item);

        var currentStatus = item.Status!;

        var newStatus = await _statusRepository.FirstOrDefaultAsync(x => x.NormalizedName == updateStatusDto.NormalizedName, token);

        NotFoundException.ThrowIfNull(newStatus);

        var currentRole = _currentUserService.Role;

        if (_currentUserService.UserId == item.UserId)
        {
            if ((currentStatus.Equals(StatusValues.Active) && newStatus.Equals(StatusValues.Inactive)) ||
                (currentStatus.Equals(StatusValues.Inactive) && newStatus.Equals(StatusValues.Active)))
            {
                item.Status = newStatus;
            }
            else
            {
                throw new ConflictException(ItemErrorMessages.StatusFailure);
            }
        }
        else if (currentRole == nameof(Role.Administrator) || currentRole == nameof(Role.Moderator))
        {
            item.Status = newStatus;
        }
        else
        {
            throw new ForbiddenException();
        }

        return await _itemRepository.UpdateAsync(item, token);
    }

    public async Task<Item> GetByIdAsync(Guid id, CancellationToken token)
    {
        var specification = new ItemWithAllSpecification(id);
        var item = await _itemRepository.FirstOrDefaultAsync(specification, token);

        NotFoundException.ThrowIfNull(item);

        return item;
    }

    public async Task<PagedList<Item>> GetAsync(ItemFilterQuery filterQuery, CancellationToken token = default)
    {
        var specification = new ItemWithAllSpecification();
        var itemQuery = _itemRepository.GetQueryable(specification);

        if (!string.IsNullOrWhiteSpace(filterQuery.SearchTerm))
        {
            itemQuery = itemQuery.Where(x => x.Title.Contains(filterQuery.SearchTerm));
        }

        if (filterQuery.CityId is not null)
        {
            itemQuery = itemQuery.Where(x => x.CityId == filterQuery.CityId);
        }

        if (filterQuery.CategoryNormalizedName is not null)
        {
            itemQuery = itemQuery.Where(x => x.Category!.NormalizedName == filterQuery.CategoryNormalizedName);
        }

        if (filterQuery.StatusNormalizedName is not null)
        {
            itemQuery = itemQuery.Where(x => x.Status!.NormalizedName == filterQuery.StatusNormalizedName);
        }

        if (filterQuery.UserId is not null)
        {
            itemQuery = itemQuery.Where(x => x.UserId == filterQuery.UserId);
        }

        if (string.Equals(filterQuery.SortColumn, nameof(SortOrder.Descending), StringComparison.OrdinalIgnoreCase))
        {
            itemQuery = itemQuery.OrderByDescending(GetSortProperty(filterQuery.SortColumn));
        }
        else
        {
            itemQuery = itemQuery.OrderBy(GetSortProperty(filterQuery.SortColumn));
        }

        var items = await PagedList<Item>.CreateAsync(itemQuery, filterQuery.Page, filterQuery.PageSize);

        return items;
    }

    private static Expression<Func<Item, object>> GetSortProperty(string? sortColumn)
    {
        if (string.Equals(sortColumn, nameof(Item.Title), StringComparison.OrdinalIgnoreCase))
        {
            return item => item.Title;
        }

        if (string.Equals(sortColumn, nameof(Item.Price), StringComparison.OrdinalIgnoreCase))
        {
            return item => item.Price;
        }

        return item => item.Id;
    }
}