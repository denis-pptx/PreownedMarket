using Identity.Domain.Entities;
using Identity.Infrastructure.Data;
using Identity.Presentation.ExceptionHandlers;
using Identity.Presentation.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Shared.Configuration;

namespace Identity.Presentation;

public static class ConfigureServices
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwagger(Const.ApplicationName);

        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services
            .AddIdentity<User, IdentityRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer();

        services
            .AddProblemDetails()
            .AddExceptionHandler<IdentityExceptionHandler>()
            .AddExceptionHandler<GlobalExceptionHandler>();

        services.AddRouting(options => options.LowercaseUrls = true);

        return services;
    }
}
