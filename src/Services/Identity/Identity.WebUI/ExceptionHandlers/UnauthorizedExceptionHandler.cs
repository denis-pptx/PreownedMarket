namespace Identity.WebUI.ExceptionHandlers;

public class UnauthorizedExceptionHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        if (exception is UnauthorizedException unauthorizedException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

            return ValueTask.FromResult(true);
        }

        return ValueTask.FromResult(false);
    }
}
