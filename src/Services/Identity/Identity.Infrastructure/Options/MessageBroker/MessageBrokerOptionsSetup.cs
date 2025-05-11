using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Identity.Infrastructure.Options.MessageBroker;

public class MessageBrokerOptionsSetup(IConfiguration configuration) 
    : IConfigureOptions<MessageBrokerOptions>
{
    public void Configure(MessageBrokerOptions options)
    {
        configuration.GetSection(nameof(MessageBrokerOptions)).Bind(options);
    }
}