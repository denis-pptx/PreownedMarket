using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Chat.Application.Options.Jwt;

public class JwtOptionsSetup(IConfiguration configuration)
    : IConfigureOptions<JwtOptions>
{
    public void Configure(JwtOptions options)
    {
        configuration.GetSection(nameof(JwtOptions)).Bind(options);
    }
}