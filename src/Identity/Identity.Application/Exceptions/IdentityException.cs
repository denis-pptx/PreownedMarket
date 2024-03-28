using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Exceptions;

public class IdentityException : ApplicationException
{
    public IEnumerable<IdentityError> Errors { get; }

    public IdentityException(IEnumerable<IdentityError> errors)
    {
        Errors = errors;
    }
}
