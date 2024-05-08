using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Item.DataAccess.Data.Initializers;

public static class DataInitializer
{
    public static async Task SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>()!;

        await CategoryInitializer.SeedAsync(dbContext);
        await LocationInitializer.SeedAsync(dbContext);
        await StatusInitializer.SeedAsync(dbContext);
        await UserInitializer.SeedAsync(dbContext);
    }
}