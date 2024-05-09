using Item.DataAccess.Caching;
using Item.DataAccess.Extensions;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;

namespace Item.DataAccess.Repositories.Cached;

public class CachedRegionRepository(
    IRegionRepository _decorated,
    ICacheService _cacheService)
    : IRegionRepository
{

    public async Task AddAsync(Region region, CancellationToken cancellationToken = default)
    {
        await _decorated.AddAsync(region, cancellationToken);

        await _cacheService.RemoveAsync(typeof(Region).GetCacheKeyWithAll(), cancellationToken);
    }

    public async Task<IEnumerable<Region>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var regions = await _cacheService.GetOrCreateAsync(
            typeof(Region).GetCacheKeyWithAll(),
            async () => await _decorated.GetAllAsync(cancellationToken),
            cancellationToken);

        return regions ?? [];
    }

    public async Task<Region?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var region = await _cacheService.GetOrCreateAsync(
           typeof(Region).GetCacheKeyWithId(id),
           async () => await _decorated.GetByIdAsync(id, cancellationToken),
           cancellationToken);

        return region;
    }

    public async Task<Region?> GetByNameAsync(string name, CancellationToken cancellationToken = default) => 
        await _decorated.GetByNameAsync(name, cancellationToken);

    public async Task RemoveAsync(Region region, CancellationToken cancellationToken = default)
    {
        await _decorated.RemoveAsync(region, cancellationToken);

        await _cacheService.RemoveAsync(region.GetCacheKeyWithId(), cancellationToken);

        await _cacheService.RemoveAsync(typeof(Region).GetCacheKeyWithAll(), cancellationToken);
    }

    public async Task UpdateAsync(Region region, CancellationToken cancellationToken = default)
    {
        await _decorated.UpdateAsync(region, cancellationToken);
         
        await _cacheService.RemoveAsync(region.GetCacheKeyWithId(), cancellationToken);

        await _cacheService.RemoveAsync(typeof(Region).GetCacheKeyWithAll(), cancellationToken);
    }
}
