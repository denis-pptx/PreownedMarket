using Identity.Domain.Models;
using Identity.Infrastructure.Data;
using Identity.Presentation.ExceptionHandlers;
using Identity.Presentation.OptionsSetup;

namespace Identity.Presentation;

public static class ConfigureServices
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

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
