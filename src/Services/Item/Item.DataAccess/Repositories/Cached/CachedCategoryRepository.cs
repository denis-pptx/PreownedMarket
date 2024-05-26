using Item.DataAccess.Caching;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;

namespace Item.DataAccess.Repositories.Cached;

public class CachedCategoryRepository(
    ICategoryRepository _decorated,
    ICacheService _cacheService) 
    : ICategoryRepository
{
    public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
    {
        await _decorated.AddAsync(category, cancellationToken);

        await _cacheService.RemoveAsync(typeof(Category).GetCacheKeyWithAll(), cancellationToken);
    }

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var key = typeof(Category).GetCacheKeyWithAll();

        var factory = async () => await _decorated.GetAllAsync(cancellationToken);

        var categories = await _cacheService.GetOrCreateAsync(key, factory, cancellationToken);

        return categories ?? [];
    }

    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var key = typeof(Category).GetCacheKeyWithId(id);

        var factory = async () => await _decorated.GetByIdAsync(id, cancellationToken);

        var category = await _cacheService.GetOrCreateAsync(key, factory, cancellationToken);

        return category;
    }

    public Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default) =>
        _decorated.GetByNameAsync(name, cancellationToken);

    public async Task RemoveAsync(Category category, CancellationToken cancellationToken = default)
    {
        await _decorated.RemoveAsync(category, cancellationToken);

        await _cacheService.RemoveAsync(category.GetCacheKeyWithId(), cancellationToken);

        await _cacheService.RemoveAsync(typeof(Category).GetCacheKeyWithAll(), cancellationToken);
    }

    public async Task UpdateAsync(Category category, CancellationToken cancellationToken = default)
    {
        await _decorated.UpdateAsync(category, cancellationToken);

        await _cacheService.RemoveAsync(category.GetCacheKeyWithId(), cancellationToken);

        await _cacheService.RemoveAsync(typeof(Category).GetCacheKeyWithAll(), cancellationToken);
    }
}
