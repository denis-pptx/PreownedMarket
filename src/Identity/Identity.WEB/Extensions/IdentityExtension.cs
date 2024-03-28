using Identity.Domain.Models;
using Identity.Infrastructure.Data;

namespace Identity.WEB.Extensions;

public static class IdentityExtension
{
    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(opt =>
                {
                    opt.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();
    }
}
