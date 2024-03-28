using Identity.Domain.Models;
using System.Security.Claims;

namespace Identity.Application.Abstractions;

public interface IJwtProvider
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromAccessToken(string accessToken);
}
