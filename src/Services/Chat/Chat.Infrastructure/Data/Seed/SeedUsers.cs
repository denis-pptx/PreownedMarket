using Chat.Application.Abstractions.Contexts;
using Chat.Domain.Entities;
using Contracts;
using Contracts.Users;
using MongoDB.Driver;

namespace Chat.Infrastructure.Data.Seed;

internal static class SeedUsers
{
    public async static Task SeedUsersAsync(this IApplicationDbContext dbContext)
    {
        foreach (var role in Enum.GetNames(typeof(Role)))
        {
            var user = await dbContext.Users
                .Find(user => user.UserName == role)
                .FirstOrDefaultAsync();

            if (user is null)
            {
                user = new User
                {
                    Id = GuidComputer.Calculate(role),
                    UserName = role,
                };

                await dbContext.Users.InsertOneAsync(user);
            }
        }
    }
}