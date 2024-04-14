namespace Identity.Application.Features.Identity.Commands.RefreshToken;

public record RefreshTokenCommand(string AccessToken, string RefreshToken) 
    : ICommand<RefreshTokenVm>;