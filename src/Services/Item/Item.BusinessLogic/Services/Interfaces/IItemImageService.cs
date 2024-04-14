using Item.DataAccess.Models;
using Microsoft.AspNetCore.Http;

namespace Item.BusinessLogic.Services.Interfaces;

public interface IItemImageService
{
    Task SaveAttachedImagesAsync(Guid itemId, IEnumerable<IFormFile> images, CancellationToken token = default);
    Task DeleteAllAttachedImagesAsync(Guid itemId, CancellationToken token = default);
    Task DeleteItemImagesAsync(IEnumerable<ItemImage> images, CancellationToken token = default);
    Task<IEnumerable<ItemImage>> GetItemImagesAsync(Guid itemId, CancellationToken token = default); 
}
