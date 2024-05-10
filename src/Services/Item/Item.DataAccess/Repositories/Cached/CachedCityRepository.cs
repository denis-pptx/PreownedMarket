using Item.DataAccess.Caching;
using Item.DataAccess.Extensions;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;

namespace Item.DataAccess.Repositories.Cached;

public class CachedCityRepository(
    ICityRepository _decorated, 
    ICacheService _cacheService) 
    : ICityRepository
{
    public async Task AddAsync(City city, CancellationToken cancellationToken = default)
    {
        await _decorated.AddAsync(city, cancellationToken);

        await _cacheService.RemoveAsync(typeof(City).GetCacheKeyWithAll(), cancellationToken);
    }

    public async Task<IEnumerable<City>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var key = typeof(City).GetCacheKeyWithAll();

        var factory = async () => await _decorated.GetAllAsync(cancellationToken);

        var cities = await _cacheService.GetOrCreateAsync(key, factory, cancellationToken);

        return cities ?? [];
    }

    public async Task<City?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var key = typeof(City).GetCacheKeyWithId(id);

        var factory = async () => await _decorated.GetByIdAsync(id, cancellationToken);

        var city = await _cacheService.GetOrCreateAsync(key, factory, cancellationToken);

        return city;
    }

    public Task<City?> GetByNameAsync(string name, CancellationToken cancellationToken = default) =>
        _decorated.GetByNameAsync(name, cancellationToken);

    public async Task RemoveAsync(City city, CancellationToken cancellationToken = default)
    {
        await _decorated.RemoveAsync(city, cancellationToken);

        await _cacheService.RemoveAsync(city.GetCacheKeyWithId(), cancellationToken);

        await _cacheService.RemoveAsync(typeof(City).GetCacheKeyWithAll(), cancellationToken);
    }

    public async Task UpdateAsync(City city, CancellationToken cancellationToken = default)
    {

        await _decorated.UpdateAsync(city, cancellationToken);

        await _cacheService.RemoveAsync(city.GetCacheKeyWithId(), cancellationToken);

        await _cacheService.RemoveAsync(typeof(City).GetCacheKeyWithAll(), cancellationToken);
    }
}
