using Item.DataAccess.Data;
using Item.DataAccess.Repositories.Implementations;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.Repositories.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Item.DataAccess;

public static class ConfigureServices
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        var connection = configurationManager.GetConnectionString("MySQL");
        services.AddDbContext<ApplicationDbContext>(
            options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 3, 0))));

        services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IStatusRepository, StatusRepository>()
            .AddScoped<IRegionRepository, RegionRepository>()
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<ICityRepository, CityRepository>()
            .AddScoped<ILikeRepository, LikeRepository>()
            .AddScoped<IItemRepository, ItemRepository>()
            .AddScoped<IImageRepository, ImageRepository>()
            .AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}