using Item.DataAccess.Caching;
using Item.DataAccess.Extensions;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;

namespace Item.DataAccess.Repositories.Cached;

public class CachedCityRepository(
    ICityRepository decorated, 
    ICacheService cacheService) 
    : ICityRepository
{
    public async Task AddAsync(City city, CancellationToken cancellationToken = default)
    {
        await decorated.AddAsync(city, cancellationToken);

        await cacheService.RemoveAsync(typeof(City).GetCacheKeyWithAll(), cancellationToken);
    }

    public async Task<IEnumerable<City>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var key = typeof(City).GetCacheKeyWithAll();

        var factory = async () => await decorated.GetAllAsync(cancellationToken);

        var cities = await cacheService.GetOrCreateAsync(key, factory, cancellationToken);

        return cities ?? [];
    }

    public async Task<City?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var key = typeof(City).GetCacheKeyWithId(id);

        var factory = async () => await decorated.GetByIdAsync(id, cancellationToken);

        var city = await cacheService.GetOrCreateAsync(key, factory, cancellationToken);

        return city;
    }

    public Task<City?> GetByNameAsync(string name, CancellationToken cancellationToken = default) =>
        decorated.GetByNameAsync(name, cancellationToken);

    public async Task RemoveAsync(City city, CancellationToken cancellationToken = default)
    {
        await decorated.RemoveAsync(city, cancellationToken);

        await cacheService.RemoveAsync(city.GetCacheKeyWithId(), cancellationToken);

        await cacheService.RemoveAsync(typeof(City).GetCacheKeyWithAll(), cancellationToken);
    }

    public async Task UpdateAsync(City city, CancellationToken cancellationToken = default)
    {
        await decorated.UpdateAsync(city, cancellationToken);

        await cacheService.RemoveAsync(city.GetCacheKeyWithId(), cancellationToken);

        await cacheService.RemoveAsync(typeof(City).GetCacheKeyWithAll(), cancellationToken);
    }
}
