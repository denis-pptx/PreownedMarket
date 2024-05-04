using Identity.Application.Exceptions.ErrorMessages;
using Microsoft.AspNetCore.Http;

namespace Identity.Application.Exceptions;

public class ConflictException : BaseApiException
{
    public ConflictException(ErrorMessage? errorMessage = null) 
        : base(StatusCodes.Status409Conflict, errorMessage)
    {

    }
}