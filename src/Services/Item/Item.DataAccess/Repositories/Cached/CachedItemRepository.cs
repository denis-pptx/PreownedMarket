using Item.DataAccess.Caching;
using Item.DataAccess.Extensions;
using Item.DataAccess.Models;
using Item.DataAccess.Models.Filter;
using Item.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Item.DataAccess.Repositories.Cached;

using Item = Models.Entities.Item;

public class CachedItemRepository(
    IItemRepository decorated, 
    ICacheService cacheService) : IItemRepository
{
    private readonly DistributedCacheEntryOptions _cacheOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60),
        SlidingExpiration = TimeSpan.FromMinutes(1)
    };

    public Task AddAsync(Item item, CancellationToken cancellationToken = default) 
        => decorated.AddAsync(item, cancellationToken);

    public Task<PagedList<Item>> GetAsync(ItemFilterRequest filterRequest, CancellationToken token = default) =>
        decorated.GetAsync(filterRequest, token);

    public async Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var item = await cacheService.GetOrCreateAsync(
           typeof(Item).GetCacheKeyWithId(id),
           async () => await decorated.GetByIdAsync(id, cancellationToken),
           _cacheOptions,
           cancellationToken);

        return item;
    }

    public async Task RemoveAsync(Item item, CancellationToken cancellationToken = default)
    {
        await decorated.RemoveAsync(item, cancellationToken);

        await cacheService.RemoveAsync(item.GetCacheKeyWithId(), cancellationToken);
    }

    public async Task UpdateAsync(Item item, CancellationToken cancellationToken = default)
    {
        await decorated.UpdateAsync(item, cancellationToken);

        await cacheService.RemoveAsync(item.GetCacheKeyWithId(), cancellationToken);
    }
}
