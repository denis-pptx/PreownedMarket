using Item.DataAccess.Models.Entities;

namespace Item.DataAccess.Repositories.Interfaces;

using Item = Models.Entities.Item;

public interface ILikeRepository
{
    Task<Like?> GetByItemAndUserAsync(Guid itemId, Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Item>> GetLikedByUserItemsAsync(Guid userId, CancellationToken token = default);
    Task AddAsync(Like like, CancellationToken cancellationToken = default);
    Task RemoveAsync(Like like, CancellationToken cancellationToken = default);
}