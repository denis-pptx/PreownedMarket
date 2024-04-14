namespace Identity.Application.Features.Identity.Commands.LoginUser;

public record LoginUserCommand(string Email, string Password)
    : ICommand<LoginUserVm>;