namespace Identity.Application.Options.Jwt;

public class JwtOptions
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public int AccessTokenLifeTimeMinutes { get; set; }
    public int RefreshTokenLifeTimeMinutes { get; set; }
}