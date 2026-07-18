using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Infrastructure.Interfaces;

namespace PreSchoolManagement.Infrastructure.Services;

public class FileStorageService : IFileStorageService
{
    private readonly string rootPath;

    public FileStorageService()
    {
        rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files");
        Directory.CreateDirectory(rootPath);
    }

    public async Task<FileUploadResult> SaveAsync(Stream stream, string originalFileName,
    string folderName, CancellationToken cancellationToken = default)
    {
        var uploadFolder = Path.Combine(rootPath, folderName);

        // Create specific folder
        Directory.CreateDirectory(uploadFolder);
        var extension = Path.GetExtension(originalFileName);
        var fileName = $"{Guid.NewGuid()}{extension}";
        var fullPath = Path.Combine(uploadFolder, fileName);

        await using var fileStream = new FileStream(fullPath, FileMode.Create);

        await stream.CopyToAsync(fileStream, cancellationToken);

        return new FileUploadResult
        {
            FileName = fileName,
            FilePath = $"/Files/{folderName}/{fileName}"
        };
    }

    public Task DeleteAsync(string? filePath, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return Task.CompletedTask;

        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath.TrimStart('/'));

        if (File.Exists(fullPath))
            File.Delete(fullPath);

        return Task.CompletedTask;
    }
}