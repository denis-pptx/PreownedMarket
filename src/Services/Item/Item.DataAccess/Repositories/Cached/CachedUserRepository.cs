using Item.DataAccess.Caching;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Options.Cache;
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

    public void Add(User user)
    {
        _decorated.Add(user);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _cacheService.GetOrCreateAsync(
            typeof(User).GetCacheKey(id), 
            async () => await _decorated.GetByIdAsync(id, cancellationToken), 
            _cacheOptions, 
            cancellationToken);
    }

    public void Remove(User user)
    {
        _decorated.Remove(user);
    }
}