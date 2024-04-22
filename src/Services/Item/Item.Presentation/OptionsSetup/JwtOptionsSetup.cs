using Item.BusinessLogic.Authentication;
using Microsoft.Extensions.Options;

namespace Item.Presentation.OptionsSetup;

public class JwtOptionsSetup(IConfiguration _configuration)
    : IConfigureOptions<JwtOptions>
{
    private const string _sectionName = "Jwt";

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(_sectionName).Bind(options);
    }
}