using Identity.Application.Abstractions;
using Identity.Application.Abstractions.Messaging;
using Identity.Domain.Entities;
using Identity.Domain.Exceptions.ErrorMessages;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Errors.Exceptions;

namespace Identity.Application.Features.Users.Commands.UpdateUserRole;

public class UpdateUserRoleHandler(
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager, 
    ICurrentUserService currentUserService) 
    : ICommandHandler<UpdateUserRoleCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserRoleCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var userActorId = currentUserService.UserId;

        if (userActorId == request.UserId.ToString())
        {
            throw new ConflictException(RoleErrorMessages.UpdateYourself);
        }

        var user = await userManager.FindByIdAsync(request.UserId.ToString());

        if (user is null)
        {
            throw new NotFoundException(UserErrorMessages.NotFound);
        }

        var currentRole = (await userManager.GetRolesAsync(user)).SingleOrDefault();
        var newRole = (await roleManager.FindByNameAsync(request.NewRole))?.Name;

        if (newRole is null)
        {
            throw new NotFoundException(RoleErrorMessages.NotFound);
        }

        if (currentRole is not null)
        {
            await userManager.RemoveFromRoleAsync(user, currentRole);
        }
            
        await userManager.AddToRoleAsync(user, newRole);

        return Unit.Value;
    }
}