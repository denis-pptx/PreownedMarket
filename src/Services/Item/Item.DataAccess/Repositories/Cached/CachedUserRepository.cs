using Item.DataAccess.Caching;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Item.DataAccess.Repositories.Cached;

public class CachedUserRepository(
    IUserRepository _decorated, 
    ICacheService _cacheService) 
    : IUserRepository
{
    private readonly DistributedCacheEntryOptions _cacheOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60),
        SlidingExpiration = TimeSpan.FromMinutes(1)
    };

    public async Task AddAsync(User user, CancellationToken cancellationToken = default) =>
        await _decorated.AddAsync(user, cancellationToken);

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _cacheService.GetOrCreateAsync(
            typeof(User).GetCacheKeyWithId(id), 
            async () => await _decorated.GetByIdAsync(id, cancellationToken), 
            _cacheOptions, 
            cancellationToken);
    }

    public async Task RemoveAsync(User user, CancellationToken cancellationToken = default)
    {
        await _decorated.RemoveAsync(user, cancellationToken);

        await _cacheService.RemoveAsync(user.GetCacheKeyWithId(), cancellationToken);

    }
}