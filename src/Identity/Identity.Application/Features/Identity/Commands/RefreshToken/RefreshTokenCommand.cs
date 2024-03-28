using Identity.Application.Abstractions.Messaging;
using Identity.Application.Features.AuthenticaIdentitytion.Commands.RefreshToken;

namespace Identity.Application.Features.Identity.Commands.RefreshToken;

public record RefreshTokenCommand(string AccessToken, string RefreshToken) 
    : ICommand<RefreshTokenVm>;
