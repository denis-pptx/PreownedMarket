namespace Identity.Application.Features.Identity.Commands.RegisterUser;

public record RegisterUserCommand(RegisterUserRequest Request)
    : ICommand<Unit>;