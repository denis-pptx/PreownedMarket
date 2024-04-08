using Item.BusinessLogic.Exceptions;
using Item.BusinessLogic.Exceptions.ErrorMessages;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Models;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.Specifications.Implementations;

namespace Item.BusinessLogic.Services.Implementations;

using Item = DataAccess.Models.Item;

public class LikeService(
    ILikeRepository _likeRepository, 
    IRepository<Item> _itemRepository,
    ICurrentUserService _currentUserService) 
    : ILikeService
{
    public async Task<IEnumerable<Item>> GetLikedItemsAsync(CancellationToken token = default)
    {
        var currentUserId = _currentUserService.UserId ?? 
            throw new UnauthorizedException();

        var itemSpecification = new ItemWithAllSpecification();

        var likedItems = await _likeRepository.GetByUserIdAsync(currentUserId, itemSpecification, token);

        return likedItems;
    }

    public async Task LikeItemAsync(Guid itemId, CancellationToken token = default)
    {
        var currentUserId = _currentUserService.UserId ?? 
            throw new UnauthorizedException();

        var item = await _itemRepository.SingleOrDefaultAsync(x => x.Id == itemId, token) ?? 
            throw new NotFoundException(GenericErrorMessages<Item>.NotFound);

        if (item.UserId == currentUserId)
        {
            throw new ConflictException(LikeErrorMessages.LikeYourItem);
        }

        var existingLike = await _likeRepository.FirstOrDefaultAsync(
            x => x.ItemId.Equals(itemId) && x.UserId.Equals(currentUserId), 
            token);

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
        var currentUserId = _currentUserService.UserId ??
            throw new UnauthorizedException();

        var existingLike = await _likeRepository.FirstOrDefaultAsync(
           x => x.ItemId.Equals(itemId) && x.UserId.Equals(currentUserId),
           token);

        if (existingLike is null)
        {
            throw new ConflictException(LikeErrorMessages.NotLiked);
        }

        await _likeRepository.DeleteAsync(existingLike, token);
    }
}
