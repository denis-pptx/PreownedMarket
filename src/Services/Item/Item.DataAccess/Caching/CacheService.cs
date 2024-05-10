using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Item.DataAccess.Caching;

public class CacheService(IDistributedCache _distributedCache) 
    : ICacheService
{
    private readonly DistributedCacheEntryOptions _defaultCacheOptions = new();

    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        ReferenceHandler = ReferenceHandler.Preserve
    };

    private readonly ConcurrentDictionary<string, bool> _cacheKeys = [];

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) 
        where T : class
    {
        var cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);

        if (cachedValue is null)
        {
            return null;
        }

        var value = JsonSerializer.Deserialize<T>(cachedValue, _serializerOptions);
        
        return value;
    }

    public async Task<T?> GetOrCreateAsync<T>(
        string key, 
        Func<Task<T?>> factory, 
        DistributedCacheEntryOptions options, 
        CancellationToken cancellationToken = default) 
        where T : class
    {
        var cachedValue = await GetAsync<T>(key, cancellationToken);

        if (cachedValue is null)
        {
            cachedValue = await factory();

            if (cachedValue is not null)
            {
                await SetAsync(key, cachedValue, options, cancellationToken);
            }
        }

        return cachedValue;
    }

    public Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T?>> factory, CancellationToken cancellationToken = default) 
        where T : class => 
        GetOrCreateAsync(key, factory, _defaultCacheOptions, cancellationToken);

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default) 
    {
        await _distributedCache.RemoveAsync(key, cancellationToken);

        _cacheKeys.TryRemove(key, out _);   
    }

    public async Task SetAsync<T>(
        string key, 
        T value, 
        DistributedCacheEntryOptions options, 
        CancellationToken cancellationToken = default)
        where T : class
    {
        string cacheValue = JsonSerializer.Serialize(value, _serializerOptions);

        await _distributedCache.SetStringAsync(
            key, 
            cacheValue, 
            options, 
            cancellationToken);

        _cacheKeys.TryAdd(key, default);
    }

    public Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) 
        where T : class => 
        SetAsync(key, value, _defaultCacheOptions, cancellationToken);

    public async Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default)
    {
        IEnumerable<Task> tasks = _cacheKeys
            .Keys
            .Where(key => key.StartsWith(prefixKey))
            .Select(key => RemoveAsync(key, cancellationToken));

        await Task.WhenAll(tasks);
    }
}