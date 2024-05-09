using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Item.DataAccess.Options.Cache;

public class CacheOptionsSetup(IConfiguration _configuration)
    : IConfigureOptions<CacheOptions>
{
    public void Configure(CacheOptions options)
    {
        _configuration.GetSection(nameof(CacheOptions)).Bind(options);
    }
}