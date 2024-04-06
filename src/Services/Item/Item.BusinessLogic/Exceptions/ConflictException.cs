using Item.BusinessLogic.Exceptions.ErrorMessages;
using Microsoft.AspNetCore.Http;

namespace Item.BusinessLogic.Exceptions;

public class ConflictException : BaseApiException
{
    public ConflictException(ErrorMessage? errorMessage = default) 
        : base(StatusCodes.Status409Conflict, errorMessage)
    {

    }
}