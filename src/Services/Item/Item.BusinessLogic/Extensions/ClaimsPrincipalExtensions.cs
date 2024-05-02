using System.Security.Claims;

namespace Item.BusinessLogic.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        var userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);

        return Guid.TryParse(userId, out Guid parsedUserId) ?
            parsedUserId :
            throw new ApplicationException("User id is unavailable");
    }


    public static string GetUserRole(this ClaimsPrincipal? principal)
    {
        var userRole = principal?.FindFirstValue(ClaimTypes.Role);

        return userRole ??
            throw new ApplicationException("User role is unavailable");
    }
}
