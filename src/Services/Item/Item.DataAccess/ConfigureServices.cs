using Item.DataAccess.Caching;
using Item.DataAccess.Data;
using Item.DataAccess.Options.Cache;
using Item.DataAccess.Repositories.Cached;
using Item.DataAccess.Repositories.Implementations;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.UnitOfWork;
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
            .AddScoped<IUnitOfWork, EfUnitOfWork>()
            .AddScoped<ICategoryRepository, CategoryRepository>().Decorate<ICategoryRepository, CachedCategoryRepository>()
            .AddScoped<IUserRepository, UserRepository>().Decorate<IUserRepository, CachedUserRepository>()
            .AddScoped<IStatusRepository, StatusRepository>().Decorate<IStatusRepository, CachedStatusRepository>()
            .AddScoped<IRegionRepository, RegionRepository>().Decorate<IRegionRepository, CachedRegionRepository>()
            .AddScoped<IItemRepository, ItemRepository>().Decorate<IItemRepository, CachedItemRepository>()
            .AddScoped<ILikeRepository, LikeRepository>().Decorate<ILikeRepository, CachedLikeRepository>()
            .AddScoped<IImageRepository, ImageRepository>().Decorate<IImageRepository, CachedImageRepository>()
            .AddScoped<ICityRepository, CityRepository>().Decorate<ICityRepository, CachedCityRepository>();

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
            redisOptions.InstanceName = $"{cacheOptions.InstanceName}-";
        });

        return services;
    }
}