using Item.DataAccess.Models.Entities;

namespace Item.DataAccess.Options.Cache;

public static class CacheExtensions
{
    public static string GetCacheKey<T>(this T entity) 
        where T : BaseEntity => 
        GetCacheKey(typeof(T), entity.Id);

    public static string GetCacheKey(this Type type, Guid id)
    {
        var key = $"{type.Name.ToLower()}-{id}";

        return key;
    }
}