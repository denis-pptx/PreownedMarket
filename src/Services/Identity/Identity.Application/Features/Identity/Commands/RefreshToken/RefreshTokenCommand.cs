namespace Identity.Application.Features.Identity.Commands.RefreshToken;

public record RefreshTokenCommand(RefreshTokenRequest Request) 
    : ICommand<RefreshTokenResponse>;