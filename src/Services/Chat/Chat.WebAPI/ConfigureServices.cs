using Chat.WebAPI.ExceptionHandlers;
using Item.Presentation.ExceptionHandlers;

namespace Chat.WebAPI;

public static class ConfigureServices
{
    public static void AddWebUIServices(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<BaseApiExceptionHandler>();
        services.AddProblemDetails();

        services.AddHttpContextAccessor();
    }
}