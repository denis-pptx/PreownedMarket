using Microsoft.AspNetCore.Http;

namespace Item.BusinessLogic.Exceptions;

public class BadRequestException : BaseApiException
{
    public BadRequestException(string message)
        : base(StatusCodes.Status400BadRequest, message)
    {

    }

    public BadRequestException()
        : base(StatusCodes.Status400BadRequest)
    {

    }
}
