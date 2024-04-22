namespace Identity.Application.Exceptions;

public class UnauthorizedException : BaseApiException
{
    public UnauthorizedException(ErrorMessage? errorMessage = null) 
        : base(StatusCodes.Status401Unauthorized, errorMessage)
    {

    }
}