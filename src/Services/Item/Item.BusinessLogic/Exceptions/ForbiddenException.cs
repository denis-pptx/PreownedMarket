using Microsoft.AspNetCore.Http;

namespace Item.BusinessLogic.Exceptions;

public class ForbiddenException : BaseApiException
{
    public ForbiddenException(string message)
        : base(StatusCodes.Status403Forbidden, message)
    {

    }

    public ForbiddenException()
        : base(StatusCodes.Status403Forbidden)
    {

    }
}
