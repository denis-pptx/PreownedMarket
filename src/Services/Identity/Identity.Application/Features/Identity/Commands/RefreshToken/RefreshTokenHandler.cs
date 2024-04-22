﻿namespace Identity.Application.Features.Identity.Commands.RefreshToken;

public class RefreshTokenHandler(IJwtProvider _jwtProvider, UserManager<User> _userManager)
    : ICommandHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var principal = _jwtProvider.GetPrincipalFromAccessToken(request.AccessToken);

        var userId = principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId is null)
        {
            throw new UnauthorizedException();
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null || 
            user.RefreshToken != request.RefreshToken || 
            user.RefreshExpiryTime < DateTime.Now)
        {
            throw new UnauthorizedException();
        }

        var accessToken = await _jwtProvider.GenerateAccessTokenAsync(user);
        var refreshTokenModel = _jwtProvider.GenerateRefreshToken();
      
        user.RefreshToken = refreshTokenModel.Token;
        user.RefreshExpiryTime = refreshTokenModel.ExpiryTime;
        await _userManager.UpdateAsync(user);

        return new RefreshTokenResponse(accessToken, refreshTokenModel.Token);
    }
}