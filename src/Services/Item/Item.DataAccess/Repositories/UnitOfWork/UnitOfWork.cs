
using Item.DataAccess.Caching;
using Item.DataAccess.Data;
using Item.DataAccess.Extensions;
using Item.DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Item.DataAccess.Repositories.UnitOfWork;

public class UnitOfWork(
    ApplicationDbContext _dbContext, 
    ICacheService _cacheService) 
    : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await RemoveChangedEntitiesFromCacheAsync(cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task RemoveChangedEntitiesFromCacheAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Task> tasks = _dbContext
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