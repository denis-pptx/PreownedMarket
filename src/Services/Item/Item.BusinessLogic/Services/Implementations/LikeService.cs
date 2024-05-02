using Item.BusinessLogic.Exceptions;
using Item.BusinessLogic.Exceptions.ErrorMessages;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.Repositories.UnitOfWork;

namespace Item.BusinessLogic.Services.Implementations;

using Item = DataAccess.Models.Entities.Item;

public class LikeService(
    IUnitOfWork _unitOfWork,
    ILikeRepository _likeRepository, 
    IItemRepository _itemRepository,
    ICurrentUserService _currentUserService) 
    : ILikeService
{
    public async Task<IEnumerable<Item>> GetLikedItemsAsync(CancellationToken cancellationToken = default)
    {
        var userId = _currentUserService.UserId;

        var items = await _itemRepository.GetLikedByUserAsync(userId, cancellationToken);

        return items;
    }

    public async Task LikeItemAsync(Guid itemId, CancellationToken cancellationToken = default)
    {
        var item = await _itemRepository.GetByIdAsync(itemId, cancellationToken);
        NotFoundException.ThrowIfNull(item);

        var userId = _currentUserService.UserId!;

        if (item.UserId == userId)
        {
            throw new ConflictException(LikeErrorMessages.LikeYourItem);
        }

        var existingLike = await _likeRepository.GetByItemAndUserAsync(itemId, userId, cancellationToken);

        if (existingLike is not null)
        {
            throw new ConflictException(LikeErrorMessages.AlreadyLiked);
        }

        var like = new Like
        {
            UserId = userId,
            ItemId = itemId
        };

        _likeRepository.Add(like);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UnlikeItemAsync(Guid itemId, CancellationToken cancellationToken = default)
    {
        var userId = _currentUserService.UserId;

        var existingLike = await _likeRepository.GetByItemAndUserAsync(itemId, userId, cancellationToken);

        if (existingLike is null)
        {
            throw new ConflictException(LikeErrorMessages.NotLiked);
        }

        _likeRepository.Remove(existingLike);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
