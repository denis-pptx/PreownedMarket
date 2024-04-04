using Item.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Item.DataAccess.Data.Initializers;

public static class CategoryInitializer
{
    public static void SeedCategoris(this ModelBuilder modelBuilder)
    {
        List<Region> categories = [
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

        modelBuilder.Entity<Region>().HasData(categories);
    }
}
