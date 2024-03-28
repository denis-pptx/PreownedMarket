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

        await userManager.AddToRoleAsync(user, nameof(Role.User));

        return Unit.Value;
    }
}