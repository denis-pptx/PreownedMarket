using Chat.Application.Abstractions.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Infrastructure.Data.Seed;

public static class DataInitializer
{
    public static async Task SeedDataAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

        await dbContext.SeedUsersAsync();
    }
}