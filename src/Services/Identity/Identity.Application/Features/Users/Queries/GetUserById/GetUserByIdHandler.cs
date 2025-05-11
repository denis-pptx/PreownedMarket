using Identity.Application.Abstractions.Messaging;
using Identity.Application.Models.DataTransferObjects.Users.Responses;
using Identity.Domain.Entities;
using Identity.Domain.Exceptions.ErrorMessages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Errors.Exceptions;

namespace Identity.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdHandler(UserManager<User> userManager) 
    : IQueryHandler<GetUserByIdQuery, GetUserResponse>
{
    public async Task<GetUserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users
            .FirstOrDefaultAsync(x => x.Id == request.Id.ToString(), cancellationToken);
        
        if (user is null)
        {
            throw new NotFoundException(UserErrorMessages.NotFound);
        }

        var role = (await userManager.GetRolesAsync(user)).Single();

        return new GetUserResponse
        {
            Id = user.Id,
            UserName = user.UserName!,
            Email = user.Email!,
            Role = role
        };
    }
}