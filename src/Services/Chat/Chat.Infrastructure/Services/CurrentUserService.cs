using Chat.Application.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Chat.Infrastructure.Services;

public class CurrentUserService(IHttpContextAccessor _httpContextAccessor)
    : ICurrentUserService
{
    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}
