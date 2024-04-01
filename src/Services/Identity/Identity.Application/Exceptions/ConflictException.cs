using Microsoft.AspNetCore.Http;

namespace Identity.Application.Exceptions;

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
