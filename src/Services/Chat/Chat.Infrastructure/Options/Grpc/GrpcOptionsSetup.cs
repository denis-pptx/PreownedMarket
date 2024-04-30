using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Chat.Infrastructure.Options.Grpc;

public class GrpcOptionsSetup(IConfiguration _configuration) 
    : IConfigureOptions<GrpcOptions>
{
    public void Configure(GrpcOptions options)
    {
        _configuration.GetSection(nameof(GrpcOptions)).Bind(options);
    }
}