using Item.DataAccess.Caching;
using Item.DataAccess.Extensions;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;

namespace Item.DataAccess.Repositories.Cached;

public class CachedCategoryRepository(
    ICategoryRepository decorated,
    ICacheService cacheService) 
    : ICategoryRepository
{
    public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
    {
        await decorated.AddAsync(category, cancellationToken);

        await cacheService.RemoveAsync(typeof(Category).GetCacheKeyWithAll(), cancellationToken);
    }

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var key = typeof(Category).GetCacheKeyWithAll();

        var factory = async () => await decorated.GetAllAsync(cancellationToken);

        var categories = await cacheService.GetOrCreateAsync(key, factory, cancellationToken);

        return categories ?? [];
    }

    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var key = typeof(Category).GetCacheKeyWithId(id);

        var factory = async () => await decorated.GetByIdAsync(id, cancellationToken);

        var category = await cacheService.GetOrCreateAsync(key, factory, cancellationToken);

        return category;
    }

    public Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default) =>
        decorated.GetByNameAsync(name, cancellationToken);

    public async Task RemoveAsync(Category category, CancellationToken cancellationToken = default)
    {
        await decorated.RemoveAsync(category, cancellationToken);

        await cacheService.RemoveAsync(category.GetCacheKeyWithId(), cancellationToken);

        await cacheService.RemoveAsync(typeof(Category).GetCacheKeyWithAll(), cancellationToken);
    }

    public async Task UpdateAsync(Category category, CancellationToken cancellationToken = default)
    {
        await decorated.UpdateAsync(category, cancellationToken);

        await cacheService.RemoveAsync(category.GetCacheKeyWithId(), cancellationToken);

        await cacheService.RemoveAsync(typeof(Category).GetCacheKeyWithAll(), cancellationToken);
    }
}
