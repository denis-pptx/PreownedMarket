namespace Identity.Application.Abstractions;

public interface IJwtProvider
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromAccessToken(string accessToken);
}
