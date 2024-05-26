using Chat.Presentation.ExceptionHandlers;
using Chat.Presentation.Options.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Chat.Presentation;

public static class ConfigureServices
{
    public static void AddPresentationServices(this IServiceCollection services)
    {
        services.AddControllers();

        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        services
            .AddProblemDetails()
            .AddExceptionHandler<ValidationExceptionHandler>()
            .AddExceptionHandler<BaseApiExceptionHandler>();

        services.AddHttpContextAccessor();

        services.AddRouting(options => 
            options.LowercaseUrls = true);

        services
            .ConfigureOptions<JwtOptionsSetup>()
            .ConfigureOptions<JwtBearerOptionsSetup>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
    }
}