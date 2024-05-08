using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Item.BusinessLogic.Options.Jwt;

public class JwtOptionsSetup(IConfiguration _configuration)
    : IConfigureOptions<JwtOptions>
{
    private const string _sectionName = "Jwt";

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(nameof(JwtOptions)).Bind(options);
    }
}