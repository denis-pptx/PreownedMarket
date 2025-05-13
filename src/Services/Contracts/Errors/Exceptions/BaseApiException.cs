using Microsoft.AspNetCore.Http;
using Shared.Errors.Messages;

namespace Shared.Errors.Exceptions;

public abstract class BaseApiException(int statusCode, ErrorMessage? errorMessage) 
    : ApplicationException
{
    public int StatusCode { get; } = statusCode;
    public ErrorMessage? ErrorMessage { get; } = errorMessage;
}