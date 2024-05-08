using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Chat.Infrastructure.Options.MongoDb;

public class MongoDbOptionsSetup(IConfiguration _configuration)
    : IConfigureOptions<MongoDbOptions>
{
    public void Configure(MongoDbOptions options)
    {
        _configuration.GetSection(nameof(MongoDbOptions)).Bind(options);
    }
}