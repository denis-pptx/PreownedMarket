
namespace Identity.WEB.ExceptionHandlers;

public class ConflictExceptionHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is ConflictException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

            return ValueTask.FromResult(true);
        }

        return ValueTask.FromResult(false);
    }
}
