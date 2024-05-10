using Microsoft.AspNetCore.Http;
using Shared.Errors.Messages;

namespace Shared.Errors.Exceptions;

public class BadRequestException(ErrorMessage? errorMessage = default)
    : BaseApiException(StatusCodes.Status400BadRequest, errorMessage);