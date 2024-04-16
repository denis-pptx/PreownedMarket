using Chat.Application.Data;
using Chat.Application.OptionsSetup;
using Chat.Infrastructure.Data;
using Chat.Infrastructure.Data.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Chat.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.ConfigureOptions<MongoDbOptions>();

        services.AddSingleton<IApplicationDbContext, ApplicationDbContext>();   

        return services;
    }
}