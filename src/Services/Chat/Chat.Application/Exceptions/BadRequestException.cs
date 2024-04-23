using Chat.Application.Exceptions.ErrorMessages;
using Microsoft.AspNetCore.Http;

namespace Chat.Application.Exceptions;

public class BadRequestException(ErrorMessage? errorMessage = default)
    : BaseApiException(StatusCodes.Status400BadRequest, errorMessage)
{
}