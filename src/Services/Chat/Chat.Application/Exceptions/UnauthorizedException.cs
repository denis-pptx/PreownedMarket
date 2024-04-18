using Chat.Application.Exceptions.ErrorMessages;
using Identity.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace Chat.Application.Exceptions;

public class UnauthorizedException(ErrorMessage? errorMessage = null) 
    : BaseApiException(StatusCodes.Status401Unauthorized, errorMessage)
{
    public static void ThrowIfNull<T>([NotNull] T? obj)
    {
        if (obj is null)
        {
            throw new UnauthorizedException();
        }
    }
}