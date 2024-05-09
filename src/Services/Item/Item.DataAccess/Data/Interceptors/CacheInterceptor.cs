using Item.DataAccess.Caching;
using Item.DataAccess.Extensions;
using Item.DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Item.DataAccess.Data.Interceptors;

public class CacheInterceptor(ICacheService _cacheService) 
    : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            await RemoveChangedEntitiesFromCacheAsync(eventData.Context, cancellationToken);
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task RemoveChangedEntitiesFromCacheAsync(
        DbContext dbContext, 
        CancellationToken cancellationToken = default)
    {
        IEnumerable<Task> tasks = dbContext
            .ChangeTracker
            .Entries<BaseEntity>()
            .Where(entry =>
                entry.State == EntityState.Modified ||
                entry.State == EntityState.Deleted)
            .Select(async entry =>
            {
                var entity = entry.Entity;

                var key = entity.GetType().GetCacheKey(entity.Id);

                await _cacheService.RemoveAsync(key, cancellationToken);
            });

        await Task.WhenAll(tasks);
    }
}
