using Identity.Application.Abstractions;
using Identity.Infrastructure.Services;

namespace Identity.WebUI.Extensions;

public static class DependencyInjectionExtension
{
    public static void RegisterDI(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
    }
}
