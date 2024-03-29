using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Identity.Domain.Enums;

namespace Identity.Infrastructure.Data.Seed;

public static class DataInitializer
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        await SeedRoles.Seed(roleManager);
        await SeedUsers.Seed(userManager);
    }
}
