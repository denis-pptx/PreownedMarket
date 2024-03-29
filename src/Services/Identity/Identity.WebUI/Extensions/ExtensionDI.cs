using Identity.Application.Abstractions;
using Identity.Infrastructure.Services;

namespace Identity.WebUI.Extensions;

public static  class ExtensionDI
{
    public static void AddDI(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
    }
}
