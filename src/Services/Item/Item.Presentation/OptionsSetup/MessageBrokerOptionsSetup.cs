using Item.BusinessLogic.Options;
using Microsoft.Extensions.Options;

namespace Item.Presentation.OptionsSetup;

public class MessageBrokerOptionsSetup(IConfiguration _configuration) 
    : IConfigureOptions<MessageBrokerOptions>
{
    private const string _sectionName = "MessageBroker";

    public void Configure(MessageBrokerOptions options)
    {
        _configuration.GetSection(_sectionName).Bind(options);
    }
}