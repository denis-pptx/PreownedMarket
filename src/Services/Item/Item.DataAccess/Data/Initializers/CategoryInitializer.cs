using Item.DataAccess.Data.Initializers.Values;
using Item.DataAccess.Models.Entities;
using System.Reflection;

namespace Item.DataAccess.Data.Initializers;

public static class CategoryInitializer
{
    private static IEnumerable<Category> GetAllCategories()
    {
        Type type = typeof(CategoryValues);
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Static | BindingFlags.Public);

        foreach (var property in properties)
        {
            yield return (property.GetValue(obj: null) as Category)!;
        }
    }

    public static async Task SeedAsync(ApplicationDbContext dbContext)
    {
        var existingCategoryNames = dbContext.Categories.Select(c => c.Name);

        var allCategories = GetAllCategories();
        var categoriesToAdd = allCategories.Where(category => !existingCategoryNames.Contains(category.Name));

        dbContext.Categories.AddRange(categoriesToAdd);

        await dbContext.SaveChangesAsync();
    }
}