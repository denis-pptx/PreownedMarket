namespace Identity.WebUI.ExceptionHandlers;

public class IdentityExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        if (exception is IdentityException identityException)
        {
            var problemDetails = new ProblemDetails
            {
                Status = identityException.StatusCode,
                Title = ReasonPhrases.GetReasonPhrase(identityException.StatusCode),
                Detail = identityException.Message,
                Type = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
                Extensions = { ["errors"] = ConvertErrors(identityException.Errors) }
            };
            
            httpContext.Response.StatusCode = problemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        return false;
    }

    private Dictionary<string, IEnumerable<string>> ConvertErrors(IEnumerable<IdentityError> errors)
    {
        var result = new Dictionary<string, IEnumerable<string>>
        {
            [""] = errors.Select(x => x.Description)
        };

        return result;
    }
}
