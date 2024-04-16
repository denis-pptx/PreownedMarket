using Chat.Application.Exceptions;
using Chat.Application.Exceptions.ErrorMessages;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace Identity.Application.Exceptions;

public class NotFoundException : BaseApiException
{
    public NotFoundException(ErrorMessage? errorMessage = null) 
        : base(StatusCodes.Status404NotFound, errorMessage)
    {

    }

    public static void ThrowIfNull<T>([NotNull] T? obj)
    {
        if (obj is null)
        {
            throw new NotFoundException(GenericErrorMessages<T>.NotFound);
        }
    }
}