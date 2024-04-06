using Item.BusinessLogic.Exceptions.ErrorMessages;
using Microsoft.AspNetCore.Http;

namespace Item.BusinessLogic.Exceptions;

public class NotFoundException : BaseApiException
{
    public NotFoundException(ErrorMessage? errorMessage = default) 
        : base(StatusCodes.Status404NotFound, errorMessage)
    {

    }
}