using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Grpc;
using Chat.Application.Abstractions.Notifications;
using Chat.Application.Consumers.Items;
using Chat.Application.Consumers.Users;
using Chat.Domain.Repositories;
using Chat.Infrastructure.Data;
using Chat.Infrastructure.Options;
using Chat.Infrastructure.Options.Grpc;
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
        services
            .ConfigureOptions<MongoDbOptionsSetup>()
            .ConfigureOptions<MessageBrokerOptionsSetup>()
            .ConfigureOptions<GrpcOptionsSetup>();

        services
            .AddSingleton<IApplicationDbContext, ApplicationDbContext>()
            .AddScoped<IUserContext, UserContext>();

        services
            .AddScoped<IMessageNotificationService, MessageNotificationService>()
            .AddScoped<IConversationNotificationService, ConversationNofiticationService>()
            .AddScoped<IItemService, ItemService>();

        services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IConversationRepository, ConversationRepository>()
            .AddScoped<IMessageRepository, MessageRepository>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            var options = busConfigurator
                .BuildServiceProvider()
                .GetRequiredService<IOptions<MessageBrokerOptions>>()
                .Value;

            busConfigurator.AddConsumer<UserCreatedConsumer>()
                .Endpoint(x => x.InstanceId = options.InstanceId);

            busConfigurator.AddConsumer<UserDeletedConsumer>()
                .Endpoint(x => x.InstanceId = options.InstanceId);

            busConfigurator.AddConsumer<ItemDeletedConsumer>()
                .Endpoint(x => x.InstanceId = options.InstanceId);

            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(options.Host), h =>
                {
                    h.Username(options.Username);
                    h.Password(options.Password);
                });

                configurator.ConfigureEndpoints(context);
            });
        });

        services
            .AddGrpcClient<Protos.Item.ItemClient>((provider, options) =>
            {
                var grpcOptions = provider.GetRequiredService<IOptions<GrpcOptions>>().Value;
                options.Address = new Uri(grpcOptions.ItemHost);
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = 
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                return handler;
            });


        services.AddSignalR();

        return services;
    }
}