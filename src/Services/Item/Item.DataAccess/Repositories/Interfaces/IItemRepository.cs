using Item.DataAccess.Models.Filter;
using Item.DataAccess.Models;

namespace Item.DataAccess.Repositories.Interfaces;

using Item = Models.Entities.Item;

public interface IItemRepository 
{
    Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedList<Item>> GetAsync(ItemFilterRequest filterRequest, CancellationToken token = default);
    Task AddAsync(Item item, CancellationToken cancellationToken = default);
    Task UpdateAsync(Item item, CancellationToken cancellationToken = default);
    Task RemoveAsync(Item item, CancellationToken cancellationToken = default);
}