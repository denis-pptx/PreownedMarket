using Item.DataAccess.Models.Entities;

namespace Item.DataAccess.Repositories.Interfaces;

public interface IImageRepository
{
    Task<IEnumerable<ItemImage>> GetByItemIdAsync(Guid itemId, CancellationToken cancellationToken = default);
    void Add(ItemImage image);
    void Remove(ItemImage image);
}
