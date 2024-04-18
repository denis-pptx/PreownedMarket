using Chat.Application.Exceptions.ErrorMessages;
using Microsoft.AspNetCore.Http;

namespace Chat.Application.Exceptions;

public class ConflictException(ErrorMessage? errorMessage = null) 
    : BaseApiException(StatusCodes.Status409Conflict, errorMessage)
{
}