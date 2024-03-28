using Identity.Infrastructure.Authentication;
using Microsoft.Extensions.Options;

namespace Identity.WEB.OptionsSetup;

public class JwtOptionsSetup(IConfiguration configuration) 
    : IConfigureOptions<JwtOptions>
{
    private const string _sectionName = "Jwt";

    public void Configure(JwtOptions options)
    {
        configuration.GetSection(_sectionName).Bind(options);
    }
}
