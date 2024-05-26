using Item.DataAccess.Models.Entities;

namespace Item.DataAccess.Caching;

public static class CacheExtensions
{
    public static string GetCacheKeyWithId<T>(this T entity) where T : BaseEntity =>
        typeof(T).GetCacheKeyWithId(entity.Id);

    public static string GetCacheKeyWithAll(this Type type) =>
        type.GetCacheKeyWithSuffix("all");

    public static string GetCacheKeyWithId(this Type type, Guid id) =>
        type.GetCacheKeyWithSuffix(id.ToString());

    public static string GetCacheKeyWithSuffix(this Type type, string suffix)
    {
        var key = $"{type.Name.ToLower()}-{suffix}";

        return key;
    }
}