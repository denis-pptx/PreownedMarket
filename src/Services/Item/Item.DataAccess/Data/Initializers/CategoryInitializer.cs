using Item.DataAccess.Models;

namespace Item.DataAccess.Data.Initializers;

public static class CategoryInitializer
{
    private static List<Category> _categories => [
        new() { Name = "Real Estate" },
        new() { Name = "Cars and Parts" },
        new() { Name = "Appliances" },
        new() { Name = "Computers and Accessories" },
        new() { Name = "Phones and Tablets" },
        new() { Name = "Electronics" },
        new() { Name = "Men's Fashion" },
        new() { Name = "Women's Fashion" },
        new() { Name = "Beauty and Health" },
        new() { Name = "Baby and Mom Essentials" },
        new() { Name = "Furniture" },
        new() { Name = "Home Accessories" },
        new() { Name = "Repair and Construction" },
        new() { Name = "Garden and Orchard" },
        new() { Name = "Hobbies, Sports, and Tourism" },
        new() { Name = "Weddings and Parties" },
        new() { Name = "Pets" },
        new() { Name = "Jobs" },
        new() { Name = "Services" },
        new() { Name = "Other" }
    ];

    public static async Task SeedAsync(ApplicationDbContext dbContext)
    {
        var existingCategoryNames = dbContext.Categories.Select(c => c.Name);

        var categoriesToAdd = _categories.Where(category => !existingCategoryNames.Contains(category.Name));

        dbContext.Categories.AddRange(categoriesToAdd);

        await dbContext.SaveChangesAsync();
    }
}