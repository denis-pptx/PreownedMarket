using Identity.Application.Abstractions;
using Identity.Infrastructure.Data;
using Identity.Infrastructure.Options.MessageBroker;
using Identity.Infrastructure.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Identity.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<MessageBrokerOptionsSetup>();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        var connection = configuration.GetConnectionString("MySQL");
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseMySql(connection, new MySqlServerVersion(new Version(8, 3, 0))));

        services.AddHttpContextAccessor();

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();

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
