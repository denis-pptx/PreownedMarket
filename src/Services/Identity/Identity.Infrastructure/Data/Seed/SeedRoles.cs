using Microsoft.AspNetCore.Identity;
using Shared.Models;

namespace Identity.Infrastructure.Data.Seed;

public static class SeedRoles
{
    public async static Task SeedAsync(this RoleManager<IdentityRole> roleManager)
    {
        foreach (var role in Enum.GetNames(typeof(Role)))
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}