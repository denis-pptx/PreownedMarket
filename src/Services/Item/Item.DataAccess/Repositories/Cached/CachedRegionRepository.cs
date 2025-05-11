using Item.DataAccess.Caching;
using Item.DataAccess.Extensions;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;

namespace Item.DataAccess.Repositories.Cached;

public class CachedRegionRepository(
    IRegionRepository decorated,
    ICacheService cacheService)
    : IRegionRepository
{

    public async Task AddAsync(Region region, CancellationToken cancellationToken = default)
    {
        await decorated.AddAsync(region, cancellationToken);

        await cacheService.RemoveAsync(typeof(Region).GetCacheKeyWithAll(), cancellationToken);
    }

    public async Task<IEnumerable<Region>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var regions = await cacheService.GetOrCreateAsync(
            typeof(Region).GetCacheKeyWithAll(),
            async () => await decorated.GetAllAsync(cancellationToken),
            cancellationToken);

        return regions ?? [];
    }

    public async Task<Region?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var region = await cacheService.GetOrCreateAsync(
           typeof(Region).GetCacheKeyWithId(id),
           async () => await decorated.GetByIdAsync(id, cancellationToken),
           cancellationToken);

        return region;
    }

    public async Task<Region?> GetByNameAsync(string name, CancellationToken cancellationToken = default) => 
        await decorated.GetByNameAsync(name, cancellationToken);

    public async Task RemoveAsync(Region region, CancellationToken cancellationToken = default)
    {
        await decorated.RemoveAsync(region, cancellationToken);

        await cacheService.RemoveAsync(region.GetCacheKeyWithId(), cancellationToken);

        await cacheService.RemoveAsync(typeof(Region).GetCacheKeyWithAll(), cancellationToken);
    }

    public async Task UpdateAsync(Region region, CancellationToken cancellationToken = default)
    {
        await decorated.UpdateAsync(region, cancellationToken);
         
        await cacheService.RemoveAsync(region.GetCacheKeyWithId(), cancellationToken);

        await cacheService.RemoveAsync(typeof(Region).GetCacheKeyWithAll(), cancellationToken);
    }
}
