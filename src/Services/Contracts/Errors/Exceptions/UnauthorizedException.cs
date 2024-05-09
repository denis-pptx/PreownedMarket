using Microsoft.AspNetCore.Http;
using Shared.Errors.Messages;
using System.Diagnostics.CodeAnalysis;

namespace Shared.Errors.Exceptions;

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