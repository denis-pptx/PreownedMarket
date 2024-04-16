using Chat.Application.Exceptions.ErrorMessages;
using Microsoft.AspNetCore.Http;

namespace Chat.Application.Exceptions;

public class ConflictException : BaseApiException
{
    public ConflictException(ErrorMessage? errorMessage = null) 
        : base(StatusCodes.Status409Conflict, errorMessage)
    {

    }
}