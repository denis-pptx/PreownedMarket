using Item.BusinessLogic.Exceptions.ErrorMessages;
using Microsoft.AspNetCore.Http;

namespace Item.BusinessLogic.Exceptions;

public class UnauthorizedException : BaseApiException
{
    public UnauthorizedException(ErrorMessage? errorMessage = default) 
        : base(StatusCodes.Status401Unauthorized, errorMessage)
    {

    }
}