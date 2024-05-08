using AutoMapper;
using Identity.Application.Abstractions.Messaging;
using Identity.Application.Models.DataTransferObjects.Users.Responses;
using Identity.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Features.Users.Queries.GetAllUsers;

public class GetAllUsersHandler(UserManager<User> _userManager, IMapper _mapper)
    : IQueryHandler<GetAllUsersQuery, IEnumerable<GetUserResponse>>
{
    public async Task<IEnumerable<GetUserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userManager.Users.ToListAsync(cancellationToken);
        var userVms = new List<GetUserResponse>();

        foreach (var user in users)
        {
            string? role = (await _userManager.GetRolesAsync(user)).Single();

            var userResponse = _mapper.Map<GetUserResponse>(user);
            userResponse.Role = role;

            userVms.Add(userResponse);
        }

        return userVms;
    }
}