using Microsoft.Extensions.DependencyInjection;

namespace Item.DataAccess.Data.Initializers;

public static class DataInitializer
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>()!;

        await CategoryInitializer.SeedAsync(dbContext);
        await LocationInitializer.SeedAsync(dbContext);
        await StatusInitializer.SeedAsync(dbContext);
    }
}