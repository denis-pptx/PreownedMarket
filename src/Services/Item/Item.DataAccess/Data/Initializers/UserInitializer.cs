using Microsoft.EntityFrameworkCore;
using Item.DataAccess.Models.Entities;
using Shared.Models;
using Shared;

namespace Item.DataAccess.Data.Initializers;

public static class UserInitializer
{
    public static async Task SeedAsync(ApplicationDbContext dbContext)
    {
        foreach (var role in Enum.GetNames(typeof(Role)))
        {
            var userId = GuidComputer.Calculate(role);

            var user = await dbContext.Users
                .Where(user => user.Id == userId)
                .FirstOrDefaultAsync();

            if (user is null)
            {
                dbContext.Users.Add(new User 
                { 
                    Id = userId 
                });
            }
        }

        await dbContext.SaveChangesAsync();
    }
}