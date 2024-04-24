using Identity.Presentation.Middleware;

namespace Identity.Presentation.Extensions;

public static class MiddlewareExtension
{
    public static void AddMiddleware(this WebApplication app)
    {
        app.UseMiddleware<RequestLogContextMiddleware>();
    }
}