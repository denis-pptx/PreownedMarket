using Microsoft.AspNetCore.Http;

namespace Item.BusinessLogic.Exceptions;

public class ConflictException : BaseApiException
{
    public ConflictException(string message) 
        : base(StatusCodes.Status409Conflict, message)
    {

    }

    public ConflictException() 
        : base(StatusCodes.Status409Conflict)
    {
        
    }
}