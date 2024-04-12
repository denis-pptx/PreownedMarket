using Chat.Application.Data;
using Chat.Infrastructure.Data;
using Chat.Infrastructure.Data.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Chat.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.Configure<MongoDbOptions>(configuration.GetSection(nameof(MongoDbOptions)));

        services.AddSingleton<IApplicationDbContext, ApplicationDbContext>();   

        return services;
    }
}