using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Notifications;
using Chat.Domain.Repositories;
using Chat.Infrastructure.Contexts;
using Chat.Infrastructure.Options.MongoDb;
using Chat.Infrastructure.Repositories;
using Chat.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Chat.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.ConfigureOptions<MongoDbOptionsSetup>();

        services.AddSingleton<IApplicationDbContext, ApplicationDbContext>()
            .AddScoped<IUserContext, UserContext>();

        services.AddScoped<IMessageNotificationService, MessageNotificationService>()
            .AddScoped<IConversationNotificationService, ConversationNofiticationService>(); 

        services.AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IConversationRepository, ConversationRepository>()
            .AddScoped<IMessageRepository, MessageRepository>()
            .AddScoped<IItemRepository, ItemRepository>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}