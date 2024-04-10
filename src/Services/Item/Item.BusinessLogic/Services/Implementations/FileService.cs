using Item.BusinessLogic.Exceptions;
using Item.BusinessLogic.Exceptions.ErrorMessages;
using Item.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Item.BusinessLogic.Services.Implementations;

public class FileService(IWebHostEnvironment _webHostEnvironment) 
    : IFileService
{
    public const string ImagesDirectory = @"uploads\images";

    public void DeleteFile(string relativePath)
    {
        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, relativePath.TrimStart('/'));

        if (File.Exists(filePath))
            File.Delete(filePath);
    }

    public async Task<string> SaveFileAsync(IFormFile file, string directoryName, CancellationToken cancellationToken)
    {
        if (file is null || file.Length == 0)
            throw new BadRequestException(FileErrorMessages.Empty);

        var uploadDirectory = Path.Combine(_webHostEnvironment.WebRootPath, directoryName);
        if (!Directory.Exists(uploadDirectory))
            Directory.CreateDirectory(uploadDirectory);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadDirectory, fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream, cancellationToken);
        }

        var relativePath = Path.Combine(directoryName, fileName).Replace("\\", "/");

        return relativePath;
    }
}