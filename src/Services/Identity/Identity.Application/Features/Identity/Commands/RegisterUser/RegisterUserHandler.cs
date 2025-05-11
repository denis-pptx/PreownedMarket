using AutoMapper;
using Identity.Application.Abstractions.Messaging;
using Identity.Application.Models.DataTransferObjects.Identity.Requests;
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
    IMapper mapper, 
    IPublishEndpoint publishEndpoint)
    : ICommandHandler<RegisterUserCommand, Unit>
{
    public async Task<Unit> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var user = mapper.Map<RegisterUserRequest, User>(request);

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