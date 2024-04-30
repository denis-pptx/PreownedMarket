using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Chat.Infrastructure.Options;

public class MessageBrokerOptionsSetup(IConfiguration _configuration) 
    : IConfigureOptions<MessageBrokerOptions>
{
    private const string _sectionName = "MessageBroker";

    public void Configure(MessageBrokerOptions options)
    {
        _configuration.GetSection(nameof(MessageBrokerOptions)).Bind(options);
    }
}