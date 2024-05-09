﻿using Item.DataAccess.Caching;
using Item.DataAccess.Extensions;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;

namespace Item.DataAccess.Repositories.Cached;

public class CachedStatusRepository(
    IStatusRepository _decorated, 
    ICacheService _cacheService) 
    : IStatusRepository
{
    public async Task<IEnumerable<Status>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var key = "all-statuses";

        var cachedStatuses = await _cacheService.GetOrCreateAsync(
            key,
            async () => await _decorated.GetAllAsync(cancellationToken),
            cancellationToken);

        return cachedStatuses ?? [];
    }

    public async Task<Status?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var status = await _cacheService.GetOrCreateAsync(
           typeof(Status).GetCacheKey(id),
           async () => await _decorated.GetByIdAsync(id, cancellationToken),
           cancellationToken);

        return status;
    }

    public async Task<Status> GetByItemIdAsync(Guid itemId, CancellationToken cancellationToken = default) => 
        await _decorated.GetByItemIdAsync(itemId, cancellationToken);

    public async Task<Status> GetByNormalizedNameAsync(string normalizedName, CancellationToken cancellationToken = default)
    {
        var status = await _cacheService.GetOrCreateAsync(
           typeof(Status).GetCacheKey(normalizedName),
           async () => await _decorated.GetByNormalizedNameAsync(normalizedName, cancellationToken),
           cancellationToken);

        return status!;
    }
}