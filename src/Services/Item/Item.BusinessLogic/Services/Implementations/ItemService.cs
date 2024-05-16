using AutoMapper;
using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Data.Initializers.Values;
using Item.DataAccess.ErrorMessages;
using Item.DataAccess.Models;
using Item.DataAccess.Models.Filter;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.UnitOfWork;
using MassTransit;
using Shared.Errors.Exceptions;
using Shared.Events.Items;
using Shared.Models;

namespace Item.BusinessLogic.Services.Implementations;

using Item = DataAccess.Models.Entities.Item;

public class ItemService(
    IUnitOfWork _unitOfWork,
    IItemRepository _itemRepository, 
    IStatusRepository _statusRepository,
    ICurrentUserService _currentUserService,
    IItemImageService _imageService,
    IPublishEndpoint _publishEndpoint,
    IMapper _mapper) 
    : IItemService
{
    public async Task<Item> CreateAsync(ItemDto itemDto, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<ItemDto, Item>(itemDto);

        item.Status = await _statusRepository.GetByNormalizedNameAsync(StatusValues.UnderReview.NormalizedName, cancellationToken);

        item.UserId = _currentUserService.UserId;

        await _itemRepository.AddAsync(item, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        try
        {
            await _imageService.SaveAttachedImagesAsync(item.Id, itemDto.AttachedImages, cancellationToken);
        }
        catch
        {
            await DeleteByIdAsync(item.Id, cancellationToken);

            throw;
        }

        return item;
    }

    public async Task<Item> UpdateAsync(Guid id, ItemDto itemDto, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetByIdAsync(id, cancellationToken);

        NotFoundException.ThrowIfNull(item);

        var currentUserId = _currentUserService.UserId;

        var currentRole = _currentUserService.Role;

        if (!(currentUserId == item.UserId || 
            currentRole == nameof(Role.Administrator) || 
            currentRole == nameof(Role.Moderator)))
        {
            throw new ForbiddenException();
        }

        using var transaction = _unitOfWork.BeginTransaction();

        _mapper.Map(itemDto, item);

        item.Status = await _statusRepository
            .GetByNormalizedNameAsync(StatusValues.UnderReview.NormalizedName, cancellationToken);

        await _itemRepository.UpdateAsync(item, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var oldImages = await _imageService.GetItemImagesAsync(item.Id, cancellationToken);

        try
        {
            await _imageService.SaveAttachedImagesAsync(item.Id, itemDto.AttachedImages, cancellationToken);

            await _imageService.DeleteAttachedImagesAsync(oldImages, cancellationToken);

            transaction.Commit();

            return item;
        }
        catch
        {
            var allImages = await _imageService.GetItemImagesAsync(item.Id, cancellationToken);
            var imagesToDelete = allImages.ExceptBy(oldImages.Select(x => x.Id), x => x.Id);

            await _imageService.DeleteAttachedImagesAsync(imagesToDelete, cancellationToken);

            transaction.Rollback();

            throw;
        }
    }

    public async Task<Item> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetByIdAsync(id, cancellationToken);

        NotFoundException.ThrowIfNull(item);

        var currentUserId = _currentUserService.UserId;

        var currentRole = _currentUserService.Role;

        if (currentUserId == item.UserId ||
            currentRole == nameof(Role.Administrator) ||
            currentRole == nameof(Role.Moderator))
        {
            await _imageService.DeleteAllAttachedImagesAsync(item.Id, cancellationToken);

            await _itemRepository.RemoveAsync(item, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _publishEndpoint.Publish(new ItemDeletedEvent(item.Id), cancellationToken);

            return item;
        }
        else
        {
            throw new ForbiddenException();
        }
    }

    public async Task<Item> ChangeStatus(Guid id, UpdateStatusDto updateStatusDto, CancellationToken cancellationToken = default)
    {
        var item = await _itemRepository.GetByIdAsync(id, cancellationToken);
        NotFoundException.ThrowIfNull(item);

        var newStatus = await _statusRepository.GetByNormalizedNameAsync(updateStatusDto.NormalizedName, cancellationToken);
        NotFoundException.ThrowIfNull(newStatus);

        var currentRole = _currentUserService.Role;

        if (_currentUserService.UserId == item.UserId)
        {
            var currentStatus = await _statusRepository.GetByItemIdAsync(id, cancellationToken);

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

        await _itemRepository.UpdateAsync(item, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return item;
    }

    public async Task<Item> GetByIdAsync(Guid id, CancellationToken token)
    {
        var item = await _itemRepository.GetByIdAsync(id, token);

        NotFoundException.ThrowIfNull(item);

        return item;
    }

    public async Task<PagedList<Item>> GetAsync(ItemFilterRequest filterRequest, CancellationToken token = default)
    {
        var items = await _itemRepository.GetAsync(filterRequest, token);    

        return items;
    }
}