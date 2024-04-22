using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Models;

public class User : IdentityUser
{
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime? RefreshExpiryTime { get; set; }
}