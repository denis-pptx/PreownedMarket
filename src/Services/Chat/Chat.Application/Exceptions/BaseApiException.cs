using Chat.Application.Exceptions.ErrorMessages;
using Identity.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace Chat.Application.Exceptions;

public abstract class BaseApiException(int statusCode, ErrorMessage? errorMessage) 
    : ApplicationException
{
    public int StatusCode { get; } = statusCode;
    public string Type { get; } = statusCode switch
    {
        StatusCodes.Status401Unauthorized => "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
        StatusCodes.Status404NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
        StatusCodes.Status409Conflict => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
        _ => string.Empty
    };
    public ErrorMessage? ErrorMessage { get; } = errorMessage;
}