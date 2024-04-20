using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Chat.Infrastructure.Options.MongoDb;

public class MongoDbOptionsSetup(IConfiguration _configuration)
    : IConfigureOptions<MongoDbOptions>
{
    private const string _sectionName = "MongoDB";

    public void Configure(MongoDbOptions options)
    {
        _configuration.GetSection(_sectionName).Bind(options);
    }
}