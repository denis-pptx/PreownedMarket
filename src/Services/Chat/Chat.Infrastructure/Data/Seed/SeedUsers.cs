using Chat.Application.Abstractions.Contexts;
using Chat.Domain.Entities;
using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text;

namespace Chat.Infrastructure.Data.Seed;

internal static class SeedUsers
{
    private static readonly List<string> _roles = ["User", "Moderator", "Administrator"];

    public async static Task SeedUsersAsync(this IApplicationDbContext dbContext)
    {
        foreach (var role in _roles)
        {
            var user = await dbContext.Users
                .Find(user => user.UserName == role)
                .FirstOrDefaultAsync();

            if (user is null)
            {
                user = new User
                {
                    Id = ComputeGuid(role),
                    UserName = role,
                };

                await dbContext.Users.InsertOneAsync(user);
            }
        }
    }

    private static Guid ComputeGuid(string input)
    {
        byte[] hash = MD5.HashData(Encoding.UTF8.GetBytes(input));

        var guid = new Guid(hash);

        return guid;
    }
}
