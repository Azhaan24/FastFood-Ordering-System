using Microsoft.AspNetCore.Http;

namespace FastFood.Core.Interfaces;

public interface IFileService
{
    Task<string> UploadFoodImageAsync(IFormFile file);

    void DeleteFile(string fileName);
}