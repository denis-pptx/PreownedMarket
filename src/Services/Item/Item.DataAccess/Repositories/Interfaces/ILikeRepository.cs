using Item.DataAccess.Models.Entities;
using Item.DataAccess.Specifications.Interfaces;

namespace Item.DataAccess.Repositories.Interfaces;

using Item = Models.Entities.Item;

public interface ILikeRepository
{
    Task<Like?> GetByItemAndUserAsync(Guid itemId, Guid userId, CancellationToken cancellationToken = default);
    void Add(Like like);
    void Remove(Like like);
}