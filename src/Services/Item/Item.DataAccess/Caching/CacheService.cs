using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Item.DataAccess.Caching;

public class CacheService(IDistributedCache _distributedCache) 
    : ICacheService
{
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) 
        where T : class
    {
        var cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);

        if (cachedValue is null)
        {
            return null;
        }

        var value = JsonSerializer.Deserialize<T>(cachedValue);

        return value;
    }

    public async Task<T?> GetOrCreateAsync<T>(
        string key, 
        Func<Task<T>> factory, 
        DistributedCacheEntryOptions options, 
        CancellationToken cancellationToken = default) 
        where T : class
    {
        var cachedValue = await GetAsync<T>(key, cancellationToken);

        if (cachedValue is not null)
        {
            return cachedValue;
        }

        cachedValue = await factory();

        await SetAsync(
            key, 
            cachedValue, 
            options, 
            cancellationToken);

        return cachedValue;
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default) 
    {
        await _distributedCache.RemoveAsync(key, cancellationToken);
    }

    public async Task SetAsync<T>(
        string key, 
        T value, 
        DistributedCacheEntryOptions options, 
        CancellationToken cancellationToken = default)
        where T : class
    {
        string cacheValue = JsonSerializer.Serialize(value);

        await _distributedCache.SetStringAsync(
            key, 
            cacheValue, 
            options, 
            cancellationToken);
    }
}