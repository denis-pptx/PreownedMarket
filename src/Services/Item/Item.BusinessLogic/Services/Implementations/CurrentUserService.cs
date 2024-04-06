using Item.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Item.BusinessLogic.Services.Implementations;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) 
    : ICurrentUserService
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;
    public Guid? UserId
    {
        get
        {
            var stringId = _httpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            return stringId is null ? null : Guid.Parse(stringId);
        }
    }

    public string? Role => _httpContext.User?.FindFirstValue(ClaimTypes.Role);
}