using Item.BusinessLogic.Services.Implementations;
using Item.BusinessLogic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Item.BusinessLogic;

public static class ConfigureServices
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {
        services
            .AddScoped<IStatusService, StatusService>()
            .AddScoped<IRegionService, RegionService>()
            .AddScoped<ICityService, CityService>()
            .AddScoped<ICategoryService, CategoryService>()
            .AddScoped<ILikeService, LikeService>()
            .AddScoped<IItemService, ItemService>();

        return services;
    }
}