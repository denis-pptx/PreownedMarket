using Chat.Application.Abstractions;
using Chat.Application.Data;
using Chat.Domain.Repositories;
using Chat.Infrastructure.Data.Contexts;
using Chat.Infrastructure.OptionsSetup;
using Chat.Infrastructure.Repositories;
using Chat.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.ConfigureOptions<MongoDbOptionsSetup>();

        services.AddSingleton<IApplicationDbContext, ApplicationDbContext>();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IConversationRepository, ConversationRepository>();  
        services.AddScoped<IMessageRepository, MessageRepository>();  

        return services;
    }
}