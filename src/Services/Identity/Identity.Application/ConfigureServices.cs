using Identity.Application.Abstractions;
using Identity.Application.Behaviours;
using Identity.Application.Features.Identity.Commands.LoginUser;
using Identity.Application.Mappings;
using Identity.Application.Options.Jwt;
using Identity.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Identity.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.ConfigureOptions<JwtOptionsSetup>();

        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(LoginUserHandler));
            cfg.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
        });

        services.AddAutoMapper(Assembly.GetAssembly(typeof(UserMappingProfile)));

        return services;
    }
}
