using Identity.WebUI.Middleware;

namespace Identity.WebUI.Extensions;

public static class MiddlewareExtension
{
    public static void AddMiddleware(this WebApplication app)
    {
        app.UseMiddleware<RequestLogContextMiddleware>();
    }
}