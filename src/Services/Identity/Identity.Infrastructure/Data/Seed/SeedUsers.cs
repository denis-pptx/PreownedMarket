using Identity.Domain.Enums;
using Identity.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

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
                    Id = ComputeGuidString(role),
                    UserName = role,
                    Email = $"{role}@mail.ru",
                };

                var password = $"{role}123#A";

                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user, role);
            }
        }
    }

    private static string ComputeGuidString(string input)
    {
        byte[] hash = MD5.HashData(Encoding.UTF8.GetBytes(input));

        var guid = new Guid(hash);

        return guid.ToString();
    }
}