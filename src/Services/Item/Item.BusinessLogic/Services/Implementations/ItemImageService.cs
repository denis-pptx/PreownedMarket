using Item.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.Models.Entities;
using Shared.Errors.Exceptions;
using Item.DataAccess.UnitOfWork;

namespace Item.BusinessLogic.Services.Implementations;

public class ItemImageService(
    IFileService fileService,
    IUnitOfWork unitOfWork,
    IImageRepository imageRepository,
    IItemRepository itemRepository)
    : IItemImageService
{
    public async Task SaveAttachedImagesAsync(Guid itemId, IEnumerable<IFormFile> images, CancellationToken cancellationToken = default)
    {
        foreach (var image in images)
        {
            var imagePath = await fileService.SaveFileAsync(image, FileService.ImagesDirectory, cancellationToken);

            var itemImage = new ItemImage
            {
                FilePath = imagePath,
                ItemId = itemId,
            };

            await imageRepository.AddAsync(itemImage, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteAllAttachedImagesAsync(Guid itemId, CancellationToken cancellationToken = default)
    {
        var item = await itemRepository.GetByIdAsync(itemId, cancellationToken);
        NotFoundException.ThrowIfNull(item);

        var images = await imageRepository.GetByItemIdAsync(item.Id, cancellationToken);

        await DeleteAttachedImagesAsync(images, cancellationToken);
    }

    public async Task DeleteAttachedImagesAsync(IEnumerable<ItemImage> images, CancellationToken cancellationToken = default)
    {
        foreach (var image in images.ToList())
        {
            fileService.DeleteFile(image.FilePath);

            await imageRepository.RemoveAsync(image);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<IEnumerable<ItemImage>> GetItemImagesAsync(Guid itemId, CancellationToken cancellationToken = default)
    {
        var item = await itemRepository.GetByIdAsync(itemId, cancellationToken);
        NotFoundException.ThrowIfNull(item);

        var images = await imageRepository.GetByItemIdAsync(item.Id, cancellationToken);

        return images;
    }
}