namespace Identity.Application.Features.Identity.Commands.LoginUser;

public record LoginUserCommand(LoginUserRequest Request)
    : ICommand<LoginUserResponse>;