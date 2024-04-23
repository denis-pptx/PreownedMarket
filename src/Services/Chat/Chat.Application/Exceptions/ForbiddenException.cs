using Chat.Application.Exceptions.ErrorMessages;
using Microsoft.AspNetCore.Http;

namespace Chat.Application.Exceptions;

public class ForbiddenException(ErrorMessage? errorMessage = default) 
    : BaseApiException(StatusCodes.Status403Forbidden, errorMessage)
{
}