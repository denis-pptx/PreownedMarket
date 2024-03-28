using Identity.WEB.ExceptionHandlers;

namespace Identity.WEB.Extensions;

public static class ExceptionExtension
{
    public static void AddExcepitonHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<IdentityExceptionHandler>();
        services.AddExceptionHandler<UnauthorizedExceptionHandler>();
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<ConflictExceptionHandler>();
    }
}
