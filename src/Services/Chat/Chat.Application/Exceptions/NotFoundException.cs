using Chat.Application.Exceptions;
using Chat.Application.Exceptions.ErrorMessages;
using Microsoft.AspNetCore.Http;

namespace Identity.Application.Exceptions;

public class NotFoundException : BaseApiException
{
    public NotFoundException(ErrorMessage? errorMessage = null) 
        : base(StatusCodes.Status404NotFound, errorMessage)
    {

    }
}