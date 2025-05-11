using Identity.Application.Abstractions;
using Identity.Application.Models;
using Identity.Application.Options.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Identity.Domain.Entities;

namespace Identity.Application.Services;

public class JwtProvider(
    IOptions<JwtOptions> jwtOptions,
    IOptions<JwtBearerOptions> jwtBearerOptions,
    UserManager<User> userManager)
    : IJwtProvider
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    private readonly JwtBearerOptions _jwtBearerOptions = jwtBearerOptions.Value;

    public async Task<string> GenerateAccessTokenAsync(User user)
    {
        var role = (await userManager.GetRolesAsync(user)).Single();

        var claims = new Claim[]
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName ?? ""),
            new(ClaimTypes.Role, role)
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            null,
            expires: DateTime.Now.AddMinutes(_jwtOptions.AccessTokenLifeTimeMinutes),
            signingCredentials: signingCredentials);

        var tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }

    public RefreshTokenModel GenerateRefreshToken()
    {
        var randomNumber = new byte[64];

        using var numberGenerator = RandomNumberGenerator.Create();
        numberGenerator.GetBytes(randomNumber);

        var tokenString = Convert.ToBase64String(randomNumber);
        var tokenLifeTime = DateTime.Now.AddMinutes(_jwtOptions.RefreshTokenLifeTimeMinutes);

        return new RefreshTokenModel(tokenString, tokenLifeTime);
    }

    public ClaimsPrincipal? GetPrincipalFromAccessToken(string accessToken)
    {
        var tokenValidationParameters = _jwtBearerOptions.TokenValidationParameters;

        return new JwtSecurityTokenHandler().ValidateToken(accessToken, tokenValidationParameters, out _);
    }
}