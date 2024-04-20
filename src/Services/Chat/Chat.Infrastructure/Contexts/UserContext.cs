using Chat.Application.Abstractions.Contexts;
using Chat.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace Chat.Infrastructure.Contexts;

public class UserContext(IHttpContextAccessor httpContextAccessor) 
    : IUserContext
{
    public Guid UserId => 
        httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ?? 
        throw new ApplicationException("User context is unavailable");
}