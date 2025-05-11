using Identity.Application.Abstractions;
using Identity.Application.Abstractions.Messaging;
using Identity.Application.Models.DataTransferObjects.Identity.Responses;
using Identity.Domain.Entities;
using Identity.Domain.Exceptions.ErrorMessages;
using Microsoft.AspNetCore.Identity;
using Shared.Errors.Exceptions;

namespace Identity.Application.Features.Identity.Commands.LoginUser;

public class LoginUserHandler(UserManager<User> userManager, IJwtProvider jwtProvider)
    : ICommandHandler<LoginUserCommand, LoginUserResponse>
{
    public async Task<LoginUserResponse> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            throw new UnauthorizedException(UserErrorMessages.IncorrectCredentials);
        }

        var isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);

        if (!isPasswordValid)
        {
            throw new UnauthorizedException(UserErrorMessages.IncorrectCredentials);
        }

        var accessToken = await jwtProvider.GenerateAccessTokenAsync(user);
        var refreshTokenModel = jwtProvider.GenerateRefreshToken();

        user.RefreshToken = refreshTokenModel.Token;
        user.RefreshExpiryTime = refreshTokenModel.ExpiryTime;

        await userManager.UpdateAsync(user);

        return new LoginUserResponse(accessToken, refreshTokenModel.Token);
    }
}