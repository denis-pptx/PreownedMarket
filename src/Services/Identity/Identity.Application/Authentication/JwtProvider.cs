using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Application.Authentication;

public class JwtProvider(
    IOptions<JwtOptions> _jwtOptions,
    IOptions<JwtBearerOptions> _jwtBearerOptions, 
    UserManager<User> _userManager) 
    : IJwtProvider
{
    private readonly JwtOptions _jwtOptions = _jwtOptions.Value;
    private readonly JwtBearerOptions _jwtBearerOptions = _jwtBearerOptions.Value;

    public async Task<string> GenerateAccessTokenAsync(User user)
    {
        string role = (await _userManager.GetRolesAsync(user)).Single();

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

        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }

    public RefreshTokenModel GenerateRefreshToken()
    {
        var randomNumber = new byte[64];

        using var numberGenerator = RandomNumberGenerator.Create();
        numberGenerator.GetBytes(randomNumber);

        string tokenString = Convert.ToBase64String(randomNumber);
        DateTime tokenLifeTime = DateTime.Now.AddMinutes(_jwtOptions.RefreshTokenLifeTimeMinutes);

        return new(tokenString, tokenLifeTime);
    }

    public ClaimsPrincipal? GetPrincipalFromAccessToken(string accessToken)
    {
        var tokenValidationParameters = _jwtBearerOptions.TokenValidationParameters;

        return new JwtSecurityTokenHandler().ValidateToken(accessToken, tokenValidationParameters, out _);
    }
}