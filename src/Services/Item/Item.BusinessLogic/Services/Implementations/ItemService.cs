using AutoMapper;
using Item.BusinessLogic.Exceptions;
using Item.BusinessLogic.Exceptions.ErrorMessages;
using Item.BusinessLogic.Models.Common;
using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Data.Initializers.Values;
using Item.DataAccess.Enums;
using Item.DataAccess.Models;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.Specifications.Implementations;
using System.Linq.Expressions;

namespace Item.BusinessLogic.Services.Implementations;

using Item = DataAccess.Models.Item;

public class ItemService(
    IRepository<Item> _itemRepository, 
    IRepository<Status> _statusRepository,
    ICurrentUserService _currentUserService,
    IMapper _mapper) 
    : IItemService
{
    public async Task<Item> CreateAsync(ItemDto itemDto, CancellationToken token)
    {
        var item = _mapper.Map<ItemDto, Item>(itemDto);

        item.CreatedAt = DateTime.UtcNow;

        var status = await _statusRepository.SingleOrDefaultAsync(
            x => x.NormalizedName == StatusValues.UnderReview.NormalizedName, token);
        item.StatusId = status!.Id;

        var currentUserId = _currentUserService.UserId ?? throw new UnauthorizedException();
        item.UserId = currentUserId;

        var result = await _itemRepository.AddAsync(item, token);

        return result;
    }

    public async Task<Item> UpdateAsync(Guid id, ItemDto itemDto, CancellationToken token)
    {
        var item = await _itemRepository.GetByIdAsync(id, token) ?? 
            throw new NotFoundException(GenericErrorMessages<Item>.NotFound);

        var currentUserId = _currentUserService.UserId ?? 
            throw new UnauthorizedException();

        var currentRole = _currentUserService.Role ?? 
            throw new UnauthorizedException();

        if (currentUserId == item.UserId ||
            currentRole == nameof(Role.Administrator) ||
            currentRole == nameof(Role.Moderator))
        {
            _mapper.Map(itemDto, item);

            var status = await _statusRepository.SingleOrDefaultAsync(
                x => x.NormalizedName == StatusValues.UnderReview.NormalizedName, token);

            item.StatusId = status!.Id;

            var result = await _itemRepository.UpdateAsync(item, token);

            return result;
        }
        else
        {
            throw new ForbiddenException();
        }
    }

    public async Task<Item> DeleteByIdAsync(Guid id, CancellationToken token)
    {
        var item = await _itemRepository.GetByIdAsync(id, token) ?? 
            throw new NotFoundException(GenericErrorMessages<Item>.NotFound);

        var currentUserId = _currentUserService.UserId ??
            throw new UnauthorizedException();

        var currentRole = _currentUserService.Role ??
            throw new UnauthorizedException();

        if (currentUserId == item.UserId ||
            currentRole == nameof(Role.Administrator) ||
            currentRole == nameof(Role.Moderator))
        {
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
        var item = await _itemRepository.FirstOrDefaultAsync(specification, token) ??
            throw new NotFoundException(GenericErrorMessages<Item>.NotFound);

        Status currentStatus = item.Status!;

        Status newStatus = await _statusRepository.FirstOrDefaultAsync(x => x.NormalizedName == updateStatusDto.NormalizedName, token) ??
            throw new NotFoundException(GenericErrorMessages<Status>.NotFound);

        var currentUserId = _currentUserService.UserId ?? 
            throw new UnauthorizedException();

        var currentRole = _currentUserService.Role ?? 
            throw new UnauthorizedException();

        if (currentUserId == item.UserId)
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
        else if (currentRole == nameof(Role.Administrator) || 
            currentRole == nameof(Role.Moderator))
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

        if (item is null)
        {
            throw new NotFoundException(GenericErrorMessages<Item>.NotFound);
        }

        return item;
    }

    public async Task<PagedList<Item>> GetAsync(
        string? searchTerm, 
        Guid? cityId,
        string? categoryNormalizedName,
        string? statusNormalizedName,
        Guid? userId,
        string? sortColumn, 
        string? sortOrder, 
        int page, 
        int pageSize, 
        CancellationToken token = default)
    {
        var specification = new ItemWithAllSpecification();
        var itemQuery = _itemRepository.GetQueryable(specification);

        if (!string.IsNullOrWhiteSpace(searchTerm))
            itemQuery = itemQuery.Where(x => x.Title.Contains(searchTerm));

        if (cityId is not null)
            itemQuery = itemQuery.Where(x => x.CityId == cityId);

        if (categoryNormalizedName is not null)
            itemQuery = itemQuery.Where(x => x.Category!.NormalizedName == categoryNormalizedName);

        if (statusNormalizedName is not null)
            itemQuery = itemQuery.Where(x => x.Status!.NormalizedName == statusNormalizedName);

        if (userId is not null)
            itemQuery = itemQuery.Where(x => x.UserId == userId);

        if (string.Equals(sortOrder, nameof(SortOrder.Descending), StringComparison.OrdinalIgnoreCase))
        {
            itemQuery = itemQuery.OrderByDescending(GetSortProperty(sortColumn));
        }
        else
        {
            itemQuery = itemQuery.OrderBy(GetSortProperty(sortColumn));
        }

        var items = await PagedList<Item>.CreateAsync(itemQuery, page, pageSize);

        return items;
    }

    private static Expression<Func<Item, object>> GetSortProperty(string? sortColumn)
    {
        if (string.Equals(sortColumn, nameof(Item.Title), StringComparison.OrdinalIgnoreCase))
            return item => item.Title;

        if (string.Equals(sortColumn, nameof(Item.Price), StringComparison.OrdinalIgnoreCase))
            return item => item.Price;

        return item => item.Id;
    }
}