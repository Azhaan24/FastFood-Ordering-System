using FastFood.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace FastFood.Infrastructure.Services;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _environment;

    public FileService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> UploadFoodImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new Exception("Invalid image.");

        string uploads =
            Path.Combine(
                _environment.WebRootPath,
                "uploads",
                "foods");

        if (!Directory.Exists(uploads))
            Directory.CreateDirectory(uploads);

        string extension = Path.GetExtension(file.FileName);

        string fileName =
            $"{Guid.NewGuid()}{extension}";

        string path =
            Path.Combine(uploads, fileName);

        using var stream =
            new FileStream(path, FileMode.Create);

        await file.CopyToAsync(stream);

        return $"/uploads/foods/{fileName}";
    }

    public void DeleteFile(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return;

        string path =
            Path.Combine(
                _environment.WebRootPath,
                fileName.TrimStart('/'));

        if (File.Exists(path))
            File.Delete(path);
    }
}