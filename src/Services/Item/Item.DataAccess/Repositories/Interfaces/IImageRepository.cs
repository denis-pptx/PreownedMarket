using Item.DataAccess.Models.Entities;

namespace Item.DataAccess.Repositories.Interfaces;

public interface IImageRepository
{
    Task<IEnumerable<ItemImage>> GetByItemIdAsync(Guid itemId, CancellationToken cancellationToken = default);
    Task AddAsync(ItemImage image, CancellationToken cancellationToken = default);
    Task RemoveAsync(ItemImage image, CancellationToken cancellationToken = default);
}
