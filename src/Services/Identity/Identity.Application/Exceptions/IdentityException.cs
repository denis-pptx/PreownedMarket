using Microsoft.AspNetCore.Http;

namespace Identity.Application.Exceptions;

public class IdentityException : BaseApiException
{
    public IEnumerable<IdentityError> Errors { get; }

    public IdentityException(IEnumerable<IdentityError> errors) 
        : base(StatusCodes.Status401Unauthorized)
    {
        Errors = errors;
    }
}
