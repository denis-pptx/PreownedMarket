using Identity.Application.Abstractions;
using Identity.Application.Authentication;
using Identity.Application.Behaviours;
using Identity.Domain.Models;
using Identity.Infrastructure.Data;
using Identity.Infrastructure.Options;
using Identity.Infrastructure.Services;
using Identity.Presentation.ExceptionHandlers;
using Identity.Presentation.OptionsSetup;
using Identity.Presentations.OptionsSetup;
using MassTransit;

namespace Identity.Presentation.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(opt =>
        {
            opt.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>();

        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.ConfigureOptions<MessageBrokerOptionsSetup>();

        services.AddAuthentication(options => 
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer();

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddMediatR(cfg => 
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(LoginUserHandler));
            cfg.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
        });

        services.AddExceptionHandler<IdentityExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();

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