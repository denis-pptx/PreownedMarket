namespace Identity.Infrastructure.Services;

public class CurrentUserService(IHttpContextAccessor _httpContextAccessor) 
    : ICurrentUserService
{
    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}