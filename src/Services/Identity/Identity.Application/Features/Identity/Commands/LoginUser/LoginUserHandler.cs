namespace Identity.Application.Features.Identity.Commands.LoginUser;

public class LoginUserHandler(UserManager<User> _userManager, IJwtProvider _jwtProvider)
    : ICommandHandler<LoginUserCommand, LoginUserResponse>
{
    public async Task<LoginUserResponse> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            throw new UnauthorizedException(UserErrorMessages.IncorrectCredentials);
        }

        bool isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!isPasswordValid)
        {
            throw new UnauthorizedException(UserErrorMessages.IncorrectCredentials);
        }

        var accessToken = await _jwtProvider.GenerateAccessTokenAsync(user);
        var refreshTokenModel = _jwtProvider.GenerateRefreshToken();

        user.RefreshToken = refreshTokenModel.Token;
        user.RefreshExpiryTime = refreshTokenModel.ExpiryTime;

        await _userManager.UpdateAsync(user);

        return new LoginUserResponse(accessToken, refreshTokenModel.Token);
    }
}