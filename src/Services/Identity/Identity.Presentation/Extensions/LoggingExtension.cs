using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

namespace Identity.Presentation.Extensions;

public static class LoggingExtension
{
    public static void UseLogging(this ConfigureHostBuilder host)
    {
        host.UseSerilog((context, loggerConfig) =>
        {
            var application = Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-");
            var environment = context.HostingEnvironment.EnvironmentName.ToLower().Replace(".", "-");
            var elasticUri = new Uri(context.Configuration["ElasticConfiguration:Uri"]!);

            loggerConfig.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(elasticUri)
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{application}-{environment}-{DateTime.UtcNow:yyyy-MM}",
                NumberOfReplicas = 1,
                NumberOfShards = 2
            })
            .Enrich.WithProperty("Environment", environment)
            .ReadFrom.Configuration(context.Configuration);
        });
    }
}