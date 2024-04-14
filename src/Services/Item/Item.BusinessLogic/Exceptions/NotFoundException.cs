using Item.BusinessLogic.Exceptions.ErrorMessages;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace Item.BusinessLogic.Exceptions;

public class NotFoundException : BaseApiException
{
    public NotFoundException(ErrorMessage? errorMessage = default) 
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