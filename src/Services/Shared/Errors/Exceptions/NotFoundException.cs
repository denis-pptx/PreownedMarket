using Microsoft.AspNetCore.Http;
using Shared.Errors.Messages;
using System.Diagnostics.CodeAnalysis;

namespace Shared.Errors.Exceptions;

public class NotFoundException(ErrorMessage? errorMessage = null) 
    : BaseApiException(StatusCodes.Status404NotFound, errorMessage)
{
    public static void ThrowIfNull<T>([NotNull] T? obj)
    {
        if (obj is null)
        {
            throw new NotFoundException(GenericErrorMessages<T>.NotFound);
        }
    }
}