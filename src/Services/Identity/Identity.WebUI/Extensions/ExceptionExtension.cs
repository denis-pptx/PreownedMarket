using Identity.WebUI.ExceptionHandlers;

namespace Identity.WebUI.Extensions;

public static class ExceptionExtension
{
    public static void AddExcepitonHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<IdentityExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
    }
}
