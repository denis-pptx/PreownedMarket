using Identity.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Identity.Presentations.OptionsSetup;

public class MessageBrokerOptionsSetup(IConfiguration _configuration) 
    : IConfigureOptions<MessageBrokerOptions>
{
    private const string _sectionName = "MessageBroker";

    public void Configure(MessageBrokerOptions options)
    {
        _configuration.GetSection(_sectionName).Bind(options);
    }
}