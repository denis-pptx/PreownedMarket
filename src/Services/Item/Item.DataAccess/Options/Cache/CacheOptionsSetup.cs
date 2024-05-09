using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Item.DataAccess.Options.Cache;

public class CacheOptionsSetup(IConfiguration _configuration)
    : IConfigureOptions<CacheOptionsSetup>
{
    public void Configure(CacheOptionsSetup options)
    {
        _configuration.GetSection(nameof(CacheOptionsSetup)).Bind(options);
    }
}