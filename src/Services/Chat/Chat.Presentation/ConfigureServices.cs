using Chat.Presentation.ExceptionHandlers;

namespace Chat.Presentation;

public static class ConfigureServices
{
    public static void AddWebUIServices(this IServiceCollection services)
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
    }
}