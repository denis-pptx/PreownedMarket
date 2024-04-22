using Item.DataAccess.Data;
using Item.DataAccess.Models;
using Item.DataAccess.Models.Enums;
using Item.DataAccess.Models.Filter;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.Specifications.Common;
using Item.DataAccess.Specifications.Implementations.Item;
using Item.DataAccess.Specifications.Interfaces;
using System.Linq.Expressions;

namespace Item.DataAccess.Repositories.Implementations;

using Item = Models.Entities.Item;

public class ItemRepository(ApplicationDbContext dbContext)
    : EfRepository<Item>(dbContext), IItemRepository
{
    public async Task<PagedList<Item>> GetAsync(
        ItemFilterRequest filter, 
        ISpecification<Item> specification, 
        CancellationToken token = default)
    {
        var query = _dbContext.Items.ApplySpecification(specification);

        if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
        {
            query = query.Where(x => x.Title.Contains(filter.SearchTerm));
        }

        if (filter.CityId is not null)
        {
            query = query.Where(x => x.CityId == filter.CityId);
        }

        if (filter.CategoryNormalizedName is not null)
        {
            query = query.Where(x => x.Category!.NormalizedName == filter.CategoryNormalizedName);
        }

        if (filter.StatusNormalizedName is not null)
        {
            query = query.Where(x => x.Status!.NormalizedName == filter.StatusNormalizedName);
        }

        if (filter.UserId is not null)
        {
            query = query.Where(x => x.UserId == filter.UserId);
        }

        if (string.Equals(filter.SortColumn, nameof(SortOrder.Descending), StringComparison.OrdinalIgnoreCase))
        {
            query = query.OrderByDescending(GetSortProperty(filter.SortColumn));
        }
        else
        {
            query = query.OrderBy(GetSortProperty(filter.SortColumn));
        }

        var items = await PagedList<Item>.CreateAsync(query, filter.Page, filter.PageSize);

        return items;
    }

    private static Expression<Func<Item, object>> GetSortProperty(string? sortColumn)
    {
        if (string.Equals(sortColumn, nameof(Item.Title), StringComparison.OrdinalIgnoreCase))
        {
            return item => item.Title;
        }

        if (string.Equals(sortColumn, nameof(Item.Price), StringComparison.OrdinalIgnoreCase))
        {
            return item => item.Price;
        }

        if (string.Equals(sortColumn, nameof(Item.CreatedAt), StringComparison.OrdinalIgnoreCase))
        {
            return item => item.CreatedAt;
        }

        return item => item.Id;
    }
}