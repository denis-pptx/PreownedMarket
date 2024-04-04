using Item.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Item.BusinessLogic.Services.Implementations;

public class CurrentUserService(IHttpContextAccessor _httpContextAccessor) 
    : ICurrentUserService
{
    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}
