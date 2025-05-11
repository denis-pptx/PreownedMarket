using Item.DataAccess.Caching;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Item.DataAccess.Repositories.Cached;

public class CachedImageRepository(
    IImageRepository decorated, 
    ICacheService cacheService) 
    : IImageRepository
{
    private readonly DistributedCacheEntryOptions _cacheOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60),
        SlidingExpiration = TimeSpan.FromMinutes(1)
    };

    public async Task AddAsync(ItemImage image, CancellationToken cancellationToken = default)
    {
        await decorated.AddAsync(image, cancellationToken);

        await cacheService.RemoveAsync(GetImagesByItemIdCacheKey(image.Id), cancellationToken);
    }

    public async Task<IEnumerable<ItemImage>> GetByItemIdAsync(Guid itemId, CancellationToken cancellationToken = default)
    {
        var key = GetImagesByItemIdCacheKey(itemId);

        var factory = async () => await decorated.GetByItemIdAsync(itemId, cancellationToken);

        var images = await cacheService.GetOrCreateAsync(key, factory, _cacheOptions, cancellationToken);

        return images ?? [];
    }

    public async Task RemoveAsync(ItemImage image, CancellationToken cancellationToken = default)
    {
        await decorated.RemoveAsync(image, cancellationToken);

        await cacheService.RemoveAsync(GetImagesByItemIdCacheKey(image.Id), cancellationToken);
    }

    public static string GetImagesByItemIdCacheKey(Guid itemId) =>
        $"item-images-{itemId}";
}