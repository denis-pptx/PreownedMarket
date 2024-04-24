using Serilog.Context;

namespace Identity.Presentation.Middleware;

public class RequestLogContextMiddleware(RequestDelegate _next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        using (LogContext.PushProperty("CorrelationId", context.TraceIdentifier))

        await _next.Invoke(context);
    }
}