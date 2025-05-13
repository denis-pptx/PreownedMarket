using Chat.Presentation.ExceptionHandlers;
using Shared.Configuration;
using Shared.Errors;

namespace Chat.Presentation;

public static class ConfigureServices
{
    public static void AddPresentationServices(this IServiceCollection services)
    {
        services.AddControllers();

        services
            .AddEndpointsApiExplorer()
            .AddSwagger(Const.ApplicationName);

        services
            .AddProblemDetails()
            .AddExceptionHandler<ValidationExceptionHandler>()
            .AddExceptionHandler<BaseApiExceptionHandler>();

        services.AddHttpContextAccessor();

        services.AddRouting(options => 
            options.LowercaseUrls = true);
    }
}