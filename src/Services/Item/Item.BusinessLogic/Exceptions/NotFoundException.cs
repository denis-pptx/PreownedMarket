using Microsoft.AspNetCore.Http;

namespace Item.BusinessLogic.Exceptions;

public class NotFoundException : BaseApiException
{
    public NotFoundException(string message) 
        : base(StatusCodes.Status404NotFound, message)
    {

    }

    public NotFoundException() 
        : base(StatusCodes.Status404NotFound)
    {
        
    }
}