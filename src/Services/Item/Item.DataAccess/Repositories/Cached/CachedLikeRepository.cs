using Item.DataAccess.Caching;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Item.DataAccess.Repositories.Cached;

using Item = Models.Entities.Item;

public class CachedLikeRepository(
    ILikeRepository decorated, 
    ICacheService cacheService) 
    : ILikeRepository
{
    private readonly DistributedCacheEntryOptions _cacheOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60),
        SlidingExpiration = TimeSpan.FromMinutes(1)
    };

    public Task<Like?> GetByItemAndUserAsync(Guid itemId, Guid userId, CancellationToken cancellationToken = default) =>
        decorated.GetByItemAndUserAsync(itemId, userId, cancellationToken);

    public async Task<IEnumerable<Item>> GetLikedByUserItemsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var items = await cacheService.GetOrCreateAsync(
           GetLikedByUserCacheKey(userId),
           async () => await decorated.GetLikedByUserItemsAsync(userId, cancellationToken),
            _cacheOptions,
           cancellationToken);

        return items ?? [];
    }

    public async Task AddAsync(Like like, CancellationToken cancellationToken = default)
    {
        await decorated.AddAsync(like, cancellationToken);

        await cacheService.RemoveAsync(
            GetLikedByUserCacheKey(like.UserId), 
            cancellationToken);
    }

    public async Task RemoveAsync(Like like, CancellationToken cancellationToken = default)
    {
        await decorated.RemoveAsync(like, cancellationToken);

        await cacheService.RemoveAsync(
            GetLikedByUserCacheKey(like.UserId), 
            cancellationToken);
    }

    public static string GetLikedByUserCacheKey(Guid userId) =>
        $"items-liked-by-{userId}";
}