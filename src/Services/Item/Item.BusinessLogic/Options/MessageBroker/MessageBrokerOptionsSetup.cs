using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Item.BusinessLogic.Options.MessageBroker;

public class MessageBrokerOptionsSetup(IConfiguration _configuration) 
    : IConfigureOptions<MessageBrokerOptions>
{
    public void Configure(MessageBrokerOptions options)
    {
        _configuration.GetSection(nameof(MessageBrokerOptions)).Bind(options);
    }
}