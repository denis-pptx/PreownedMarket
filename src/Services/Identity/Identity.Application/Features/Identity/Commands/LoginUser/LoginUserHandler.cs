namespace Identity.Application.Features.Identity.Commands.LoginUser;

public class LoginUserHandler(UserManager<User> userManager, IJwtProvider jwtProvider)
    : ICommandHandler<LoginUserCommand, LoginUserVm>
{
    public async Task<LoginUserVm> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            throw new UnauthorizedException("Incorrect Email or Password");
        }

        bool isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordValid)
        {
            throw new UnauthorizedException("Incorrect Email or Password");
        }

        var accessToken = await jwtProvider.GenerateAccessTokenAsync(user);
        var refreshTokenModel = jwtProvider.GenerateRefreshToken();

        user.RefreshToken = refreshTokenModel.Token;
        user.RefreshExpiryTime = refreshTokenModel.ExpiryTime;
        await userManager.UpdateAsync(user);

        return new LoginUserVm(accessToken, refreshTokenModel.Token);
    }
}
