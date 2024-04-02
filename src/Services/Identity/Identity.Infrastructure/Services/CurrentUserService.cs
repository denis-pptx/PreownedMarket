using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.Services;

public class CurrentUserService(IHttpContextAccessor _httpContextAccessor, UserManager<User> _userManager) 
    : ICurrentUserService
{
    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public async Task<User?> GetCurrentUserAsync()
    {
        string? userId = this.UserId;
        User? user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

        return user;
    }
}
