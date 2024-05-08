using FluentValidation;
using Item.BusinessLogic.Consumers.Users;
using Item.BusinessLogic.Mappings;
using Item.BusinessLogic.Models.Validators;
using Item.BusinessLogic.Options.Jwt;
using Item.BusinessLogic.Options.MessageBroker;
using Item.BusinessLogic.Services.Implementations;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Transactions.Implementations;
using Item.DataAccess.Transactions.Interfaces;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Reflection;

namespace Item.BusinessLogic;

public static class ConfigureServices
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {
        services
            .ConfigureOptions<JwtOptionsSetup>()
            .ConfigureOptions<JwtBearerOptionsSetup>()
            .ConfigureOptions<MessageBrokerOptionsSetup>();

        services
            .AddScoped<IStatusService, StatusService>()
            .AddScoped<IRegionService, RegionService>()
            .AddScoped<ICityService, CityService>()
            .AddScoped<ICategoryService, CategoryService>()
            .AddScoped<ILikeService, LikeService>()
            .AddScoped<IItemService, ItemService>()
            .AddScoped<IItemImageService, ItemImageService>();

        services
            .AddScoped<ICurrentUserService, CurrentUserService>()
            .AddScoped<ITransactionManager, EfTransactionManager>()
            .AddScoped<IFileService, FileService>();

        services
            .AddValidatorsFromAssemblyContaining<CategoryValidator>()
            .AddFluentValidationAutoValidation();

        services.AddAutoMapper(Assembly.GetAssembly(typeof(CategoryProfile)));

        services.AddGrpc();

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            string applicationName = Assembly.GetExecutingAssembly().GetName().Name!.Split('.').First().ToLower();

            busConfigurator.AddConsumer<UserCreatedConsumer>()
                .Endpoint(x => x.InstanceId = applicationName);

            busConfigurator.AddConsumer<UserDeletedConsumer>()
                .Endpoint(x => x.InstanceId = applicationName);

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