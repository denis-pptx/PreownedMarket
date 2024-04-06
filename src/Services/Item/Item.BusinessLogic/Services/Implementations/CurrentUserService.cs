using Item.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Item.BusinessLogic.Services.Implementations;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) 
    : ICurrentUserService
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;
    public string? UserId => _httpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public string? Role => _httpContext.User?.FindFirstValue(ClaimTypes.Role);
}