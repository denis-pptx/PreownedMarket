using Identity.Infrastructure.Options.MessageBroker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Identity.Infrastructure.Options.MessageBroker;

public class MessageBrokerOptionsSetup(IConfiguration _configuration) 
    : IConfigureOptions<MessageBrokerOptions>
{
    public void Configure(MessageBrokerOptions options)
    {
        _configuration.GetSection(nameof(MessageBrokerOptions)).Bind(options);
    }
}