using Item.DataAccess.Models.Filter;
using Item.DataAccess.Models;
using Item.DataAccess.Specifications.Interfaces;

namespace Item.DataAccess.Repositories.Interfaces;

using Item = Models.Entities.Item;

public interface IItemRepository 
{
    Task<PagedList<Item>> GetAsync(ItemFilterRequest filterRequest, ISpecification<Item> specification, CancellationToken token = default);
}