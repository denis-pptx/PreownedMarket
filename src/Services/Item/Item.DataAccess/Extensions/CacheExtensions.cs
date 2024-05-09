using Item.DataAccess.Models.Entities;

namespace Item.DataAccess.Extensions;

public static class CacheExtensions
{
    public static string GetCacheKey<T>(this T entity)
        where T : BaseEntity =>
        typeof(T).GetCacheKey(entity.Id.ToString());

    public static string GetCacheKey(this Type type, Guid id) =>
        type.GetCacheKey(id.ToString());

    public static string GetCacheKey(this Type type, string suffix)
    {
        var key = $"{type.Name.ToLower()}-{suffix}";

        return key;
    }
}