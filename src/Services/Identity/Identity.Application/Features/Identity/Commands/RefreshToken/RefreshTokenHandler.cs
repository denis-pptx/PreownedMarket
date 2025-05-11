using Identity.Application.Abstractions;
using Identity.Application.Abstractions.Messaging;
using Identity.Application.Models.DataTransferObjects.Identity.Responses;
using Microsoft.AspNetCore.Identity;
using Shared.Errors.Exceptions;
using System.Security.Claims;
using Identity.Domain.Entities;

namespace Identity.Application.Features.Identity.Commands.RefreshToken;

public class RefreshTokenHandler(
    IJwtProvider jwtProvider, 
    UserManager<User> userManager)
    : ICommandHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    public async Task<RefreshTokenResponse> Handle(
        RefreshTokenCommand command, 
        CancellationToken cancellationToken)
    {
        var request = command.Request;

        var principal = jwtProvider.GetPrincipalFromAccessToken(request.AccessToken);

        var userId = principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId is null)
        {
            throw new UnauthorizedException();
        }

        var user = await userManager.FindByIdAsync(userId);

        if (user is null || 
            user.RefreshToken != request.RefreshToken || 
            user.RefreshExpiryTime < DateTime.UtcNow)
        {
            throw new UnauthorizedException();
        }

        var accessToken = await jwtProvider.GenerateAccessTokenAsync(user);
        var refreshTokenModel = jwtProvider.GenerateRefreshToken();
      
        user.RefreshToken = refreshTokenModel.Token;
        user.RefreshExpiryTime = refreshTokenModel.ExpiryTime;
        await userManager.UpdateAsync(user);

        return new RefreshTokenResponse(accessToken, refreshTokenModel.Token);
    }
}