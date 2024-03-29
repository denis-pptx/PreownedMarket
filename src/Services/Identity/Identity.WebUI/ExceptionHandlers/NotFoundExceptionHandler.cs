namespace Identity.WebUI.ExceptionHandlers;

public class NotFoundExceptionHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is NotFoundException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

            return ValueTask.FromResult(true);
        }

        return ValueTask.FromResult(false);
    }
}
