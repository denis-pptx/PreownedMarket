using Chat.Application.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Chat.Application.OptionsSetup;

public class JwtOptionsSetup(IConfiguration _configuration) 
    : IConfigureOptions<JwtOptions>
{
    private const string _sectionName = "Jwt";

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(_sectionName).Bind(options);
    }
}