using AutoMapper;
using Identity.Application.Abstractions.Messaging;
using Identity.Application.Exceptions;
using Identity.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Features.Identity.Commands.RegisterUser;

public class RegisterUserHandler(UserManager<User> userManager, IMapper mapper)
    : ICommandHandler<RegisterUserCommand, Unit>
{
    public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        User user = mapper.Map<RegisterUserCommand, User>(request);

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            throw new IdentityException(result.Errors);
        }

        return Unit.Value;
    }
}