using Microsoft.AspNetCore.Http;

namespace Identity.Application.Exceptions;

public class UnauthorizedException : BaseApiException
{
    public UnauthorizedException(string message) 
        : base(StatusCodes.Status401Unauthorized, message)
    {

    }

    public UnauthorizedException() 
        : base(StatusCodes.Status401Unauthorized) 
    { 
    
    }

}
