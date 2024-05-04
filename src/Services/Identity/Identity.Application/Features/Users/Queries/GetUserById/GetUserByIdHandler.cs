using AutoMapper;
using Identity.Application.Abstractions.Messaging;
using Identity.Application.Exceptions;
using Identity.Application.Exceptions.ErrorMessages;
using Identity.Application.Models.DataTransferObjects.Users.Responses;
using Identity.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdHandler(UserManager<User> _userManager, IMapper _mapper) 
    : IQueryHandler<GetUserByIdQuery, GetUserResponse>
{
    public async Task<GetUserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(x => x.Id == request.Id.ToString(), cancellationToken);
        
        if (user is null)
        {
            throw new NotFoundException(UserErrorMessages.NotFound);
        }

        var role = (await _userManager.GetRolesAsync(user)).Single();

        var userResponse = _mapper.Map<User, GetUserResponse>(user);
        userResponse.Role = role;

        return userResponse;
    }
}