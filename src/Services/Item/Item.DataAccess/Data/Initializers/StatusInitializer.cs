using Item.DataAccess.Data.Initializers.Values;
using Item.DataAccess.Models.Entities;
using System.Reflection;

namespace Item.DataAccess.Data.Initializers;

public static class StatusInitializer
{
    private static IEnumerable<Status> GetAllStatuses()
    {
        Type type = typeof(StatusValues);
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Static | BindingFlags.Public);

        foreach (var property in properties)
        {
            yield return (property.GetValue(obj: null) as Status)!;
        }
    }

    public static async Task SeedAsync(ApplicationDbContext dbContext)
    {
        var existingStatusNames = dbContext.Statuses.Select(c => c.NormalizedName);

        var allStatuses = GetAllStatuses();
        var statusesToAdd = allStatuses.Where(status => !existingStatusNames.Contains(status.NormalizedName));

        dbContext.Statuses.AddRange(statusesToAdd);

        await dbContext.SaveChangesAsync();
    }
}