using Item.BusinessLogic.Exceptions.ErrorMessages;
using Microsoft.AspNetCore.Http;

namespace Item.BusinessLogic.Exceptions;

public class ForbiddenException : BaseApiException
{
    public ForbiddenException(ErrorMessage? errorMessage = default)
        : base(StatusCodes.Status403Forbidden, errorMessage)
    {

    }
}