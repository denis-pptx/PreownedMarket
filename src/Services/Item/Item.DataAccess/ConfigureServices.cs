using Item.DataAccess.Repositories.Implementations;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.Repositories.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Item.DataAccess;

public static class ConfigureServices
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
    {
        services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IStatusRepository, StatusRepository>()
            .AddScoped<IRegionRepository, RegionRepository>()
            .AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}