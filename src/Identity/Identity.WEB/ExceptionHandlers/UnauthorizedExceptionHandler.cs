using Identity.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Identity.WEB.ExceptionHandlers;

public class UnauthorizedExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        if (exception is UnauthorizedException unauthorizedException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

            return true;
        }

        return false;
    }
}
