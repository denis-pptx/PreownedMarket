using Microsoft.AspNetCore.Http;
using Shared.Errors.Messages;

namespace Shared.Errors.Exceptions;

public class ConflictException(ErrorMessage? errorMessage = null) 
    : BaseApiException(StatusCodes.Status409Conflict, errorMessage);