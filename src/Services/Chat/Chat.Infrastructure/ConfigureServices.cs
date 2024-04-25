using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Notifications;
using Chat.Application.Consumers.Users;
using Chat.Domain.Repositories;
using Chat.Infrastructure.Contexts;
using Chat.Infrastructure.Options;
using Chat.Infrastructure.Options.MongoDb;
using Chat.Infrastructure.Repositories;
using Chat.Infrastructure.Services;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Chat.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.ConfigureOptions<MongoDbOptionsSetup>()
                .ConfigureOptions<MessageBrokerOptionsSetup>();

        services.AddSingleton<IApplicationDbContext, ApplicationDbContext>()
                .AddScoped<IUserContext, UserContext>();

        services.AddScoped<IMessageNotificationService, MessageNotificationService>()
                .AddScoped<IConversationNotificationService, ConversationNofiticationService>(); 

        services.AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IConversationRepository, ConversationRepository>()
                .AddScoped<IMessageRepository, MessageRepository>()
                .AddScoped<IItemRepository, ItemRepository>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            busConfigurator.AddConsumer<UserCreatedConsumer>()
                .Endpoint(x => x.InstanceId = "chat");

            busConfigurator.AddConsumer<UserDeletedConsumer>()
                .Endpoint(x => x.InstanceId = "chat");

            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                var options = context.GetRequiredService<IOptions<MessageBrokerOptions>>().Value;

                configurator.Host(new Uri(options.Host), h =>
                {
                    h.Username(options.Username);
                    h.Password(options.Password);
                });

                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}