using Microsoft.Extensions.Options;

namespace ApiGateway.Options.Jwt;

public class JwtOptionsSetup(IConfiguration _configuration)
    : IConfigureOptions<JwtOptions>
{
    public void Configure(JwtOptions options)
    {
        _configuration
            .GetSection(nameof(JwtOptions))
            .Bind(options);
    }
}