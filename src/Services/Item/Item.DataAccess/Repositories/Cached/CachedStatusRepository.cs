using Item.DataAccess.Caching;
using Item.DataAccess.Extensions;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;

namespace Item.DataAccess.Repositories.Cached;

public class CachedStatusRepository(
    IStatusRepository decorated, 
    ICacheService cacheService) 
    : IStatusRepository
{
    public async Task<IEnumerable<Status>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var cachedStatuses = await cacheService.GetOrCreateAsync(
            typeof(Status).GetCacheKeyWithAll(),
            async () => await decorated.GetAllAsync(cancellationToken),
            cancellationToken);

        return cachedStatuses ?? [];
    }

    public async Task<Status?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var status = await cacheService.GetOrCreateAsync(
           typeof(Status).GetCacheKeyWithId(id),
           async () => await decorated.GetByIdAsync(id, cancellationToken),
           cancellationToken);

        return status;
    }

    public async Task<Status> GetByItemIdAsync(Guid itemId, CancellationToken cancellationToken = default) => 
        await decorated.GetByItemIdAsync(itemId, cancellationToken);

    public async Task<Status> GetByNormalizedNameAsync(string normalizedName, CancellationToken cancellationToken = default)
    {
        var status = await cacheService.GetOrCreateAsync(
           typeof(Status).GetCacheKeyWithSuffix(normalizedName),
           async () => await decorated.GetByNormalizedNameAsync(normalizedName, cancellationToken),
           cancellationToken);

        return status!;
    }
}