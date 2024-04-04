using Microsoft.EntityFrameworkCore;

namespace Item.DataAccess.Data.Initializers;

public static class DataInitializer
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.SeedCategoris();
    }
}
