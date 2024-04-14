using Identity.Application.Abstractions;
using Identity.Application.Behaviours;
using Identity.Domain.Models;
using Identity.Infrastructure.Data;
using Identity.Infrastructure.Services;
using Identity.WebUI.ExceptionHandlers;
using Identity.WebUI.OptionsSetup;

namespace Identity.WebUI.Extensions;

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

        return services;
    }
}