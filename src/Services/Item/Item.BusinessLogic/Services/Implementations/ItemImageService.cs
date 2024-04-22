using Item.BusinessLogic.Exceptions.ErrorMessages;
using Item.BusinessLogic.Exceptions;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Specifications.Implementations.Item;
using Microsoft.AspNetCore.Http;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.Models.Entities;

namespace Item.BusinessLogic.Services.Implementations;

using Item = DataAccess.Models.Entities.Item;

public class ItemImageService(
    IFileService _fileService,
    IRepository<ItemImage> _imageRepository,
    IRepository<Item> _itemRepository)
    : IItemImageService
{
    public async Task SaveAttachedImagesAsync(Guid itemId, IEnumerable<IFormFile> images, CancellationToken token = default)
    {
        foreach (var image in images)
        {
            var imagePath = await _fileService.SaveFileAsync(image, FileService.ImagesDirectory, token);

            var itemImage = new ItemImage
            {
                FilePath = imagePath,
                ItemId = itemId,
            };

            await _imageRepository.AddAsync(itemImage, token);
        }
    }

    public async Task DeleteAllAttachedImagesAsync(Guid itemId, CancellationToken token = default)
    {
        var specification = new ItemWithImagesSpecification(itemId);

        var item = await _itemRepository.FirstOrDefaultAsync(specification, token);

        NotFoundException.ThrowIfNull(item);
            
        await DeleteAttachedImagesAsync(item.Images, token);
    }

    public async Task DeleteAttachedImagesAsync(IEnumerable<ItemImage> images, CancellationToken token = default)
    {
        foreach (var image in images.ToList())
        {
            _fileService.DeleteFile(image.FilePath);

            await _imageRepository.DeleteAsync(image, token);
        }
    }

    public async Task<IEnumerable<ItemImage>> GetItemImagesAsync(Guid itemId, CancellationToken token = default)
    {
        var item = await _itemRepository.GetByIdAsync(itemId, token);

        NotFoundException.ThrowIfNull(item);

        var images = await _imageRepository.GetAsync(x => x.ItemId == itemId, token);

        return images;
    }
}