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
    IUnitOfWork unitOfWork,
    IItemRepository itemRepository, 
    IStatusRepository statusRepository,
    ICurrentUserService currentUserService,
    IItemImageService imageService,
    IPublishEndpoint publishEndpoint) 
    : IItemService
{
    public async Task<Item> CreateAsync(ItemDto itemDto, CancellationToken cancellationToken)
    {
        var item = new Item
        {
            Title = itemDto.Title,
            Description = itemDto.Description,
            Price = itemDto.Price,
            CityId = itemDto.CityId,
            CategoryId = itemDto.CategoryId,
            UserId = currentUserService.UserId,
            Status = await statusRepository.GetByNormalizedNameAsync(StatusValues.UnderReview.NormalizedName, cancellationToken)
        };

        await itemRepository.AddAsync(item, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        try
        {
            await imageService.SaveAttachedImagesAsync(item.Id, itemDto.AttachedImages, cancellationToken);
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
        var item = await itemRepository.GetByIdAsync(id, cancellationToken);

        NotFoundException.ThrowIfNull(item);

        var currentUserId = currentUserService.UserId;

        var currentRole = currentUserService.Role;

        if (!(currentUserId == item.UserId || 
            currentRole == nameof(Role.Administrator) || 
            currentRole == nameof(Role.Moderator)))
        {
            throw new ForbiddenException();
        }

        using var transaction = unitOfWork.BeginTransaction();

        item.Title = itemDto.Title;
        item.Description = itemDto.Description;
        item.Price = itemDto.Price;
        item.CityId = itemDto.CityId;
        item.CategoryId = itemDto.CategoryId;

        item.Status = await statusRepository
            .GetByNormalizedNameAsync(StatusValues.UnderReview.NormalizedName, cancellationToken);

        await itemRepository.UpdateAsync(item, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var oldImages = await imageService.GetItemImagesAsync(item.Id, cancellationToken);

        try
        {
            await imageService.SaveAttachedImagesAsync(item.Id, itemDto.AttachedImages, cancellationToken);

            await imageService.DeleteAttachedImagesAsync(oldImages, cancellationToken);

            transaction.Commit();

            return item;
        }
        catch
        {
            var allImages = await imageService.GetItemImagesAsync(item.Id, cancellationToken);
            var imagesToDelete = allImages.ExceptBy(oldImages.Select(x => x.Id), x => x.Id);

            await imageService.DeleteAttachedImagesAsync(imagesToDelete, cancellationToken);

            transaction.Rollback();

            throw;
        }
    }

    public async Task<Item> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var item = await itemRepository.GetByIdAsync(id, cancellationToken);

        NotFoundException.ThrowIfNull(item);

        var currentUserId = currentUserService.UserId;

        var currentRole = currentUserService.Role;

        if (currentUserId == item.UserId ||
            currentRole == nameof(Role.Administrator) ||
            currentRole == nameof(Role.Moderator))
        {
            await imageService.DeleteAllAttachedImagesAsync(item.Id, cancellationToken);

            await itemRepository.RemoveAsync(item, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            await publishEndpoint.Publish(new ItemDeletedEvent(item.Id), cancellationToken);

            return item;
        }
        else
        {
            throw new ForbiddenException();
        }
    }

    public async Task<Item> ChangeStatus(Guid id, UpdateStatusDto updateStatusDto, CancellationToken cancellationToken = default)
    {
        var item = await itemRepository.GetByIdAsync(id, cancellationToken);
        NotFoundException.ThrowIfNull(item);

        var newStatus = await statusRepository.GetByNormalizedNameAsync(updateStatusDto.NormalizedName, cancellationToken);
        NotFoundException.ThrowIfNull(newStatus);

        var currentRole = currentUserService.Role;

        if (currentUserService.UserId == item.UserId)
        {
            var currentStatus = await statusRepository.GetByItemIdAsync(id, cancellationToken);

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
        else if (currentRole is nameof(Role.Administrator) or nameof(Role.Moderator))
        {
            item.Status = newStatus;
        }
        else
        {
            throw new ForbiddenException();
        }

        await itemRepository.UpdateAsync(item, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return item;
    }

    public async Task<Item> GetByIdAsync(Guid id, CancellationToken token)
    {
        var item = await itemRepository.GetByIdAsync(id, token);

        NotFoundException.ThrowIfNull(item);

        return item;
    }

    public async Task<PagedList<Item>> GetAsync(ItemFilterRequest filterRequest, CancellationToken token = default)
    {
        var items = await itemRepository.GetAsync(filterRequest, token);    

        return items;
    }
}