using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shared.Errors.Exceptions;

namespace Identity.Domain.Exceptions;

public class IdentityException : BaseApiException
{
    public IEnumerable<IdentityError> Errors { get; }

    public IdentityException(IEnumerable<IdentityError> errors) 
        : base(StatusCodes.Status401Unauthorized, errorMessage: null)
    {
        Errors = errors;
    }
}