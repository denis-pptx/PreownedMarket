using Item.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.UnitOfWork;
using Shared.Errors.Exceptions;

namespace Item.BusinessLogic.Services.Implementations;

public class ItemImageService(
    IFileService _fileService,
    IUnitOfWork _unitOfWork,
    IImageRepository _imageRepository,
    IItemRepository _itemRepository)
    : IItemImageService
{
    public async Task SaveAttachedImagesAsync(Guid itemId, IEnumerable<IFormFile> images, CancellationToken cancellationToken = default)
    {
        foreach (var image in images)
        {
            var imagePath = await _fileService.SaveFileAsync(image, FileService.ImagesDirectory, cancellationToken);

            var itemImage = new ItemImage
            {
                FilePath = imagePath,
                ItemId = itemId,
            };

            await _imageRepository.AddAsync(itemImage, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteAllAttachedImagesAsync(Guid itemId, CancellationToken cancellationToken = default)
    {
        var item = await _itemRepository.GetByIdAsync(itemId, cancellationToken);
        NotFoundException.ThrowIfNull(item);

        var images = await _imageRepository.GetByItemIdAsync(item.Id, cancellationToken);

        await DeleteAttachedImagesAsync(images, cancellationToken);
    }

    public async Task DeleteAttachedImagesAsync(IEnumerable<ItemImage> images, CancellationToken cancellationToken = default)
    {
        foreach (var image in images.ToList())
        {
            _fileService.DeleteFile(image.FilePath);

            await _imageRepository.RemoveAsync(image);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<IEnumerable<ItemImage>> GetItemImagesAsync(Guid itemId, CancellationToken cancellationToken = default)
    {
        var item = await _itemRepository.GetByIdAsync(itemId, cancellationToken);
        NotFoundException.ThrowIfNull(item);

        var images = await _imageRepository.GetByItemIdAsync(item.Id, cancellationToken);

        return images;
    }
}