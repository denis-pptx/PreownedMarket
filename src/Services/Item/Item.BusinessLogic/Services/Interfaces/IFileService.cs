using Microsoft.AspNetCore.Http;

namespace Item.BusinessLogic.Services.Interfaces;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile imageFile, string directoryName, CancellationToken cancellationToken);
    void DeleteFile(string imagePath);
}
