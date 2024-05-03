using Item.DataAccess.Models.Entities;

namespace Item.DataAccess.Repositories.Interfaces;

public interface ILikeRepository
{
    Task<Like?> GetByItemAndUserAsync(Guid itemId, Guid userId, CancellationToken cancellationToken = default);
    void Add(Like like);
    void Remove(Like like);
}