namespace Identity.Application.Features.Identity.Commands.RefreshToken;

public class RefreshTokenHandler(IJwtProvider jwtProvider, UserManager<User> userManager)
    : ICommandHandler<RefreshTokenCommand, RefreshTokenVm>
{
    public async Task<RefreshTokenVm> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var principal = jwtProvider.GetPrincipalFromAccessToken(request.AccessToken);

        var userId = principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId is null)
        {
            throw new UnauthorizedException();
        }

        var user = await userManager.FindByIdAsync(userId);
        if (user is null || 
            user.RefreshToken != request.RefreshToken || 
            user.RefreshExpiryTime < DateTime.Now)
        {
            throw new UnauthorizedException();
        }

        var accessToken = jwtProvider.GenerateAccessToken(user);
        var refreshToken = jwtProvider.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshExpiryTime = DateTime.Now.AddDays(1);
        await userManager.UpdateAsync(user);

        return new RefreshTokenVm(accessToken, refreshToken);
    }
}
