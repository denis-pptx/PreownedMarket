namespace Identity.Application.Features.Identity.Commands.RegisterUser;

public class RegisterUserHandler(UserManager<User> _userManager, IMapper _mapper)
    : ICommandHandler<RegisterUserCommand, Unit>
{
    public async Task<Unit> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        User user = _mapper.Map<RegisterUserRequest, User>(request);

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            throw new IdentityException(result.Errors);
        }

        await _userManager.AddToRoleAsync(user, nameof(Role.User));

        return Unit.Value;
    }
}