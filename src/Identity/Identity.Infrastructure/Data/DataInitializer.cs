using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Identity.Domain.Enums;
using System.Data;

namespace Identity.Infrastructure.Data;

public static class DataInitializer
{
    public static async Task SeedRoles(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var role in Enum.GetNames(typeof(Role)))
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
