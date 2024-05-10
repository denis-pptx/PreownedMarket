using Item.DataAccess.Caching;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Item.DataAccess.Repositories.Cached;

public class CachedImageRepository(
    IImageRepository _decorated, 
    ICacheService _cacheService) 
    : IImageRepository
{
    private readonly DistributedCacheEntryOptions _cacheOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60),
        SlidingExpiration = TimeSpan.FromMinutes(1)
    };

    public async Task AddAsync(ItemImage image, CancellationToken cancellationToken = default)
    {
        await _decorated.AddAsync(image, cancellationToken);

        await _cacheService.RemoveAsync(GetImagesByItemIdCacheKey(image.Id), cancellationToken);
    }

    public async Task<IEnumerable<ItemImage>> GetByItemIdAsync(Guid itemId, CancellationToken cancellationToken = default)
    {
        var key = GetImagesByItemIdCacheKey(itemId);

        var factory = async () => await _decorated.GetByItemIdAsync(itemId, cancellationToken);

        var images = await _cacheService.GetOrCreateAsync(key, factory, _cacheOptions, cancellationToken);

        return images ?? [];
    }

    public async Task RemoveAsync(ItemImage image, CancellationToken cancellationToken = default)
    {
        await _decorated.RemoveAsync(image, cancellationToken);

        await _cacheService.RemoveAsync(GetImagesByItemIdCacheKey(image.Id), cancellationToken);
    }

    public static string GetImagesByItemIdCacheKey(Guid itemId) =>
        $"item-images-{itemId}";
}