using Identity.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Shared;
using Shared.Models;

namespace Identity.Infrastructure.Data.Seed;

public static class SeedUsers
{
    public async static Task SeedAsync(this UserManager<User> userManager)
    {
        foreach (var role in Enum.GetNames(typeof(Role)))
        {
            var user = await userManager.FindByNameAsync(role);

            if (user is null)
            {
                user = new User
                {
                    Id = GuidComputer.Calculate(role).ToString(),
                    UserName = role,
                    Email = $"{role}@mail.ru",
                };

                var password = $"{role}123#A";

                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}