using Item.BusinessLogic.Exceptions.ErrorMessages;
using Microsoft.AspNetCore.Http;

namespace Item.BusinessLogic.Exceptions;

public class BadRequestException(ErrorMessage? errorMessage = default)
    : BaseApiException(StatusCodes.Status403Forbidden, errorMessage)
{
}