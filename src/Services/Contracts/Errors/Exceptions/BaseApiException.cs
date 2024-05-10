using Microsoft.AspNetCore.Http;
using Shared.Errors.Messages;

namespace Shared.Errors.Exceptions;

public abstract class BaseApiException(int statusCode, ErrorMessage? errorMessage) 
    : ApplicationException
{
    public int StatusCode { get; } = statusCode;
    public string Type { get; } = statusCode switch
    {
        StatusCodes.Status400BadRequest => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
        StatusCodes.Status401Unauthorized => "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
        StatusCodes.Status403Forbidden => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3",
        StatusCodes.Status404NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
        StatusCodes.Status409Conflict => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
        _ => string.Empty
    };
    public ErrorMessage? ErrorMessage { get; } = errorMessage;
}