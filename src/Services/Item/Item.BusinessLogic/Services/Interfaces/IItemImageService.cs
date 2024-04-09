using Microsoft.AspNetCore.Http;

namespace Item.BusinessLogic.Services.Interfaces;

public interface IItemImageService
{
    Task SaveAttachedImagesAsync(Guid itemId, IEnumerable<IFormFile> images, CancellationToken token = default);
    Task DeleteAttachedImagesAsync(Guid itemId, CancellationToken token = default);
}
