using Chat.Application.Abstractions;
using Chat.Application.Data;
using Chat.Infrastructure.Data;
using Chat.Infrastructure.Data.Contexts;
using Chat.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.ConfigureOptions<MongoDbOptions>();

        services.AddSingleton<IApplicationDbContext, ApplicationDbContext>();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}