using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace Chat.WebAPI.ExceptionHandlers;

public class ValidationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        if (exception is ValidationException validationException)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = "Validation error",
                Detail = "One or more validation errors has occurred"
            };

            if (validationException.Errors.Any())
            {
                problemDetails.Extensions["errors"] = GetErrors(validationException.Errors);
            }

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        return false;
    }

    private static Dictionary<string, string[]> GetErrors(IEnumerable<ValidationFailure> validationFailures)
    {
        return validationFailures.GroupBy(
            x => x.PropertyName, 
            x => x.ErrorMessage, 
            (propertyName, errorMessages) => new
            {
                Key = propertyName,
                Values = errorMessages.Distinct().ToArray()
            })
        .ToDictionary(x => x.Key, x => x.Values);
    }
}