using Chat.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Chat.Infrastructure.OptionsSetup;

public class MongoDbOptionsSetup(IConfiguration _configuration) 
    : IConfigureOptions<MongoDbOptions>
{
    private const string _sectionName = "MongoDB";

    public void Configure(MongoDbOptions options)
    {
        _configuration.GetSection(_sectionName).Bind(options);
    }
}