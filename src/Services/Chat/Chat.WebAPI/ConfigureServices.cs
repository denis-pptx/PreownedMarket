using Chat.WebAPI.ExceptionHandlers;
using Item.Presentation.ExceptionHandlers;

namespace Chat.WebAPI;

public static class ConfigureServices
{
    public static void AddWebUIServices(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer()
            .AddSwaggerGen();

        services.AddProblemDetails()
            .AddExceptionHandler<ValidationExceptionHandler>()
            .AddExceptionHandler<BaseApiExceptionHandler>();

        services.AddHttpContextAccessor();
    }
}