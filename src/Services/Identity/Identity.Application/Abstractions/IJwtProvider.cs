using Identity.Application.Models;
using System.Security.Claims;
using Identity.Domain.Entities;

namespace Identity.Application.Abstractions;

public interface IJwtProvider
{
    Task<string> GenerateAccessTokenAsync(User user);
    RefreshTokenModel GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromAccessToken(string accessToken);
}