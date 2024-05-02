using Item.BusinessLogic.Extensions;
using Item.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Item.BusinessLogic.Services.Implementations;

public class CurrentUserService(IHttpContextAccessor _httpContextAccessor) 
    : ICurrentUserService
{
    public Guid UserId =>
        _httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new ApplicationException("User context is unavailable");

    public string Role =>
        _httpContextAccessor
            .HttpContext?
            .User
            .GetUserRole() ?? 
        throw new ApplicationException("User context is unavailable");
}