using Item.DataAccess.Models.Entities;

namespace Item.DataAccess.Repositories.Interfaces;

public interface ILikeRepository
{
    Task<Like?> GetByItemAndUserAsync(Guid itemId, Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(Like like, CancellationToken cancellationToken = default);
    Task RemoveAsync(Like like, CancellationToken cancellationToken = default);
}