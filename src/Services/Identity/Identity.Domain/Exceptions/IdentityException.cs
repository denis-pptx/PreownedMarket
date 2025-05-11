using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shared.Errors.Exceptions;

namespace Identity.Domain.Exceptions;

public class IdentityException(IEnumerable<IdentityError> errors)
    : BaseApiException(StatusCodes.Status401Unauthorized, errorMessage: null)
{
    public IEnumerable<IdentityError> Errors { get; } = errors;
}