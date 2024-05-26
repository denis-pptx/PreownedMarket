using Item.DataAccess.Caching;
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
        var cachedStatuses = await _cacheService.GetOrCreateAsync(
            typeof(Status).GetCacheKeyWithAll(),
            async () => await _decorated.GetAllAsync(cancellationToken),
            cancellationToken);

        return cachedStatuses ?? [];
    }

    public async Task<Status?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var status = await _cacheService.GetOrCreateAsync(
           typeof(Status).GetCacheKeyWithId(id),
           async () => await _decorated.GetByIdAsync(id, cancellationToken),
           cancellationToken);

        return status;
    }

    public async Task<Status> GetByItemIdAsync(Guid itemId, CancellationToken cancellationToken = default) => 
        await _decorated.GetByItemIdAsync(itemId, cancellationToken);

    public async Task<Status> GetByNormalizedNameAsync(string normalizedName, CancellationToken cancellationToken = default)
    {
        var status = await _cacheService.GetOrCreateAsync(
           typeof(Status).GetCacheKeyWithSuffix(normalizedName),
           async () => await _decorated.GetByNormalizedNameAsync(normalizedName, cancellationToken),
           cancellationToken);

        return status!;
    }
}