namespace Identity.Application.Features.Identity.Commands.LoginUser;

public class LoginUserHandler(UserManager<User> userManager, IJwtProvider jwtProvider)
    : ICommandHandler<LoginUserCommand, LoginUserVm>
{
    public async Task<LoginUserVm> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            throw new UnauthorizedException();
        }

        bool isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordValid)
        {
            throw new UnauthorizedException();
        }

        var accessToken = jwtProvider.GenerateAccessToken(user);
        var refreshToken = jwtProvider.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshExpiryTime = DateTime.Now.AddDays(1);
        await userManager.UpdateAsync(user);

        return new LoginUserVm(accessToken, refreshToken);
    }
}
