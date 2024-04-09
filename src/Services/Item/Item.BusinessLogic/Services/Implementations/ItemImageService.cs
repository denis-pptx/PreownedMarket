using Item.BusinessLogic.Exceptions.ErrorMessages;
using Item.BusinessLogic.Exceptions;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Models;
using Item.DataAccess.Specifications.Implementations.Item;
using Microsoft.AspNetCore.Http;
using Item.DataAccess.Repositories.Interfaces;

namespace Item.BusinessLogic.Services.Implementations;

using Item = DataAccess.Models.Item;

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

    public async Task DeleteAttachedImagesAsync(Guid itemId, CancellationToken token = default)
    {
        var specification = new ItemWithImagesSpecification(itemId);

        var item = await _itemRepository.FirstOrDefaultAsync(specification, token) ??
            throw new NotFoundException(GenericErrorMessages<Item>.NotFound);

        foreach (var image in item.Images.ToList())
        {
            _fileService.DeleteFile(image.FilePath);
            await _imageRepository.DeleteAsync(image, token);
        }
    }
}