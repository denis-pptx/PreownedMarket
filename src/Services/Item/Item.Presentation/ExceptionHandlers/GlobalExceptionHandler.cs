using Item.BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace Item.Presentation.ExceptionHandlers;

public class GlobalExceptionHandler : IExceptionHandler
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
                Detail = baseException.Message,
                Type = baseException.Type,
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        return false;
    }
}
