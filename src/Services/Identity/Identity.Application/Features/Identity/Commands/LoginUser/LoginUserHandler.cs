namespace Identity.Application.Features.Identity.Commands.LoginUser;

public class LoginUserHandler(UserManager<User> _userManager, IJwtProvider _jwtProvider)
    : ICommandHandler<LoginUserCommand, LoginUserVm>
{
    public async Task<LoginUserVm> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
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

        return new LoginUserVm(accessToken, refreshTokenModel.Token);
    }
}