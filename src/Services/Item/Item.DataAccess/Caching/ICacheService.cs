﻿using Microsoft.Extensions.Caching.Distributed;

namespace Item.DataAccess.Caching;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) 
        where T : class;

    Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T?>> factory, DistributedCacheEntryOptions options, CancellationToken cancellationToken = default) 
        where T : class;

    Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options, CancellationToken cancellationToken = default)
        where T : class;

    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
}