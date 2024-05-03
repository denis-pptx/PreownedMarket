using Item.Presentation.ExceptionHandlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json.Serialization;

namespace Item.Presentation;

public static class ConfigureServices
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(x => 
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

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