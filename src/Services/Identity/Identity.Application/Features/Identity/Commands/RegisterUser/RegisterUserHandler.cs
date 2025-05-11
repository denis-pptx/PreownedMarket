using Identity.Application.Abstractions.Messaging;
using Identity.Domain.Entities;
using Identity.Domain.Exceptions;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Events.Users;
using Shared.Models;

namespace Identity.Application.Features.Identity.Commands.RegisterUser;

public class RegisterUserHandler(
    UserManager<User> userManager, 
    IPublishEndpoint publishEndpoint)
    : ICommandHandler<RegisterUserCommand, Unit>
{
    public async Task<Unit> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var user = new User
        {
            UserName = request.UserName,
            Email = request.Email
        };
        
        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            throw new IdentityException(result.Errors);
        }

        await userManager.AddToRoleAsync(user, nameof(Role.User));

        await publishEndpoint.Publish(new UserCreatedEvent
            {
                Id = Guid.Parse(user.Id),
                UserName = user.UserName!
            }, 
            cancellationToken);

        return Unit.Value;
    }
}