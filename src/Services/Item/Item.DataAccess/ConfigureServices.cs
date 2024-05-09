using Item.DataAccess.Data;
using Item.DataAccess.Options.Cache;
using Item.DataAccess.Repositories.Implementations;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.Repositories.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Item.DataAccess;

public static class ConfigureServices
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        services.ConfigureOptions<CacheOptionsSetup>();

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
        services
            .AddDistributedMemoryCache()
            .AddSingleton<ICacheService, CacheService>();

        services.AddStackExchangeRedisCache(redisOptions =>
        {
            var cacheOptions = services
                .BuildServiceProvider()
                .GetRequiredService<IOptions<CacheOptions>>()
                .Value;

            redisOptions.Configuration = cacheOptions.ConnectionString;
            redisOptions.InstanceName = cacheOptions.InstanceName;
        });

        return services;
    }
}