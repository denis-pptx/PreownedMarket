using Item.BusinessLogic.Exceptions;
using Item.BusinessLogic.Exceptions.ErrorMessages;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.Specifications.Implementations.Item;

namespace Item.BusinessLogic.Services.Implementations;

using Item = DataAccess.Models.Entities.Item;

public class LikeService(
    ILikeRepository _likeRepository, 
    IRepository<Item> _itemRepository,
    ICurrentUserService _currentUserService) 
    : ILikeService
{
    public async Task<IEnumerable<Item>> GetLikedItemsAsync(CancellationToken token = default)
    {
        var currentUserId = _currentUserService.UserId!.Value;

        var specification = new ItemWithAllSpecification();

        var likedItems = await _likeRepository.GetByUserIdAsync(currentUserId, specification, token);

        return likedItems;
    }

    public async Task LikeItemAsync(Guid itemId, CancellationToken token = default)
    {
        var item = await _itemRepository.SingleOrDefaultAsync(x => x.Id == itemId, token);

        NotFoundException.ThrowIfNull(item);

        var currentUserId = _currentUserService.UserId!.Value;

        if (item.UserId == currentUserId)
        {
            throw new ConflictException(LikeErrorMessages.LikeYourItem);
        }

        var existingLike = await _likeRepository.SingleOrDefaultAsync(
            x => x.ItemId.Equals(itemId) && x.UserId.Equals(currentUserId), token);

        if (existingLike is not null)
        {
            throw new ConflictException(LikeErrorMessages.AlreadyLiked);
        }

        var like = new Like
        {
            UserId = currentUserId,
            ItemId = itemId
        };

        await _likeRepository.AddAsync(like, token);
    }

    public async Task UnlikeItemAsync(Guid itemId, CancellationToken token = default)
    {
        var currentUserId = _currentUserService.UserId!.Value;

        var existingLike = await _likeRepository.SingleOrDefaultAsync(
            x => x.ItemId.Equals(itemId) && x.UserId.Equals(currentUserId), token);

        if (existingLike is null)
        {
            throw new ConflictException(LikeErrorMessages.NotLiked);
        }

        await _likeRepository.DeleteAsync(existingLike, token);
    }
}
