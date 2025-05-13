using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Shared.Errors.Exceptions;

namespace Shared.Errors;

public class BaseApiExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        if (exception is BaseApiException baseException)
        {
            var problemDetails = new ProblemDetails
            {
                Status = baseException.StatusCode,
                Title = ReasonPhrases.GetReasonPhrase(baseException.StatusCode),
                Detail = baseException.ErrorMessage?.Description,
                Type = baseException.ErrorMessage?.Code,
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        return false;
    }
}