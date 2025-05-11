using Identity.Application.Abstractions.Messaging;
using Identity.Application.Models.DataTransferObjects.Users.Responses;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Features.Users.Queries.GetAllUsers;

public class GetAllUsersHandler(UserManager<User> userManager)
    : IQueryHandler<GetAllUsersQuery, IEnumerable<GetUserResponse>>
{
    public async Task<IEnumerable<GetUserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userManager.Users.ToListAsync(cancellationToken);
        var responses = new List<GetUserResponse>();

        foreach (var user in users)
        {
            var role = (await userManager.GetRolesAsync(user)).Single();

            var userResponse = new GetUserResponse
            {
                Id = user.Id,
                UserName = user.UserName!,
                Email = user.Email!,
                Role = role
            };

            responses.Add(userResponse);
        }

        return responses;
    }
}