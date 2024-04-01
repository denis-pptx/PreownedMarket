using Identity.Application.Models;

namespace Identity.Application.Abstractions;

public interface IJwtProvider
{
    Task<string> GenerateAccessTokenAsync(User user);
    RefreshTokenModel GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromAccessToken(string accessToken);
}