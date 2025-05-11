using Identity.Application.Abstractions;
using Identity.Application.Abstractions.Messaging;
using Identity.Domain.Entities;
using Identity.Domain.Exceptions.ErrorMessages;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Errors.Exceptions;
using Shared.Events.Users;

namespace Identity.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserByIdHandler(
    UserManager<User> userManager, 
    ICurrentUserService userService, 
    IPublishEndpoint publishEndpoint) 
    : ICommandHandler<DeleteUserByIdCommand, Unit>
{
    public async Task<Unit> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
    {
        var userIdentity = await userManager.FindByIdAsync(request.Id.ToString());

        if (userIdentity is null)
        {
            throw new NotFoundException(UserErrorMessages.NotFound);
        }

        var userActorId = userService.UserId;

        if (userActorId == userIdentity.Id)
        {
            throw new ConflictException(UserErrorMessages.DeleteYourself);
        }

        await userManager.DeleteAsync(userIdentity);

        await publishEndpoint.Publish(
            new UserDeletedEvent 
            { 
                UserId = Guid.Parse(userIdentity.Id) 
            }, 
            cancellationToken);

        return Unit.Value;
    }
}