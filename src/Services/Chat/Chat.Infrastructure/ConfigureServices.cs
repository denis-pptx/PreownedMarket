using Chat.Application.Abstractions;
using Chat.Application.Abstractions.Contexts;
using Chat.Domain.Repositories;
using Chat.Infrastructure.Contexts;
using Chat.Infrastructure.Options.MongoDb;
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
        services.AddScoped<IUserContext, UserContext>();

        services.AddScoped<IMessageNotificationService, MessageNotificationService>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IConversationRepository, ConversationRepository>();  
        services.AddScoped<IMessageRepository, MessageRepository>();  
        services.AddScoped<IItemRepository, ItemRepository>();  

        return services;
    }
}