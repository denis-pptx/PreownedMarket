using Chat.Application.Exceptions.ErrorMessages;
using Microsoft.AspNetCore.Http;

namespace Chat.Application.Exceptions;

public class UnauthorizedException : BaseApiException
{
    public UnauthorizedException(ErrorMessage? errorMessage = null) 
        : base(StatusCodes.Status401Unauthorized, errorMessage)
    {

    }
}