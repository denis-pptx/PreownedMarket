using Item.DataAccess.Models.Filter;
using Item.DataAccess.Models;
using Item.DataAccess.Specifications.Interfaces;

namespace Item.DataAccess.Repositories.Interfaces;

using Item = Models.Entities.Item;

public interface IItemRepository 
{
    Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedList<Item>> GetAsync(ItemFilterRequest filterRequest, CancellationToken token = default);
    Task<IEnumerable<Item>> GetLikedByUserAsync(Guid userId, CancellationToken token = default);
    void Add(Item item);
    void Update(Item item);
    void Delete(Item item);
}