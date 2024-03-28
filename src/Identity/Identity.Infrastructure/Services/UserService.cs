using Microsoft.AspNetCore.Http;

namespace Identity.Infrastructure.Services;

public class UserService(IHttpContextAccessor httpContextAccessor) : IUserService
{
    private readonly ClaimsPrincipal _user = httpContextAccessor.HttpContext!.User;
    public string? GetMyId()
    {
        var id = _user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        return id;
    }
}
