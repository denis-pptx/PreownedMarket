﻿using Item.BusinessLogic.Exceptions.ErrorMessages;
using Microsoft.AspNetCore.Http;

namespace Item.BusinessLogic.Exceptions;

public abstract class BaseApiException : ApplicationException
{
    public int StatusCode { get; }
    public string Type { get; }
    public ErrorMessage? ErrorMessage { get; }

    public BaseApiException(int statusCode, ErrorMessage? errorMessage) 
    {
        StatusCode = statusCode;

        Type = statusCode switch
        {
            StatusCodes.Status401Unauthorized => "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
            StatusCodes.Status404NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            StatusCodes.Status409Conflict => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
            _ => string.Empty
        };

        ErrorMessage = errorMessage;
    }
}