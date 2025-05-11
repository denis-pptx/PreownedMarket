using Item.Presentation.ExceptionHandlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json.Serialization;
using Shared.Configuration;

namespace Item.Presentation;

public static class ConfigureServices
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonOptions(x => 
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        services
            .AddEndpointsApiExplorer()
            .AddSwagger(Const.ApplicationName);

        services.AddRouting(options => 
            options.LowercaseUrls = true);

        services.AddHttpContextAccessor();

        services
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddProblemDetails();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        return services;
    }
}