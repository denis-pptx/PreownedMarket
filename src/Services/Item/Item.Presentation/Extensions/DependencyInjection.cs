using Item.BusinessLogic.Services.Implementations;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Repositories.Implementations;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.Transactions.Implementations;
using Item.DataAccess.Transactions.Interfaces;
using Item.Presentation.OptionsSetup;

namespace Item.Presentation.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IRegionService, RegionService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IStatusService, StatusService>();
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<ILikeService, LikeService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IItemImageService, ItemImageService>();

        services.AddScoped<ITransactionManager, EfTransactionManager>();

        services.AddScoped<ILikeRepository, LikeRepository>();  
        services.AddScoped<IItemRepository, ItemRepository>();  
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();

        return services;
    }
}
