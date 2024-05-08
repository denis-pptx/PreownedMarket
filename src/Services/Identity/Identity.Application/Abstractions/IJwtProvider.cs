using Identity.Application.Models;
using Identity.Domain.Models;
using System.Security.Claims;

namespace Identity.Application.Abstractions;

public interface IJwtProvider
{
    Task<string> GenerateAccessTokenAsync(User user);
    RefreshTokenModel GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromAccessToken(string accessToken);
}