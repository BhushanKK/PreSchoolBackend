using PreSchoolManagement.Domain.Models;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IFileStorageService
{
    Task<FileUploadResult> SaveAsync(
        Stream stream,
        string originalFileName,
        string folderName,
        CancellationToken cancellationToken = default);


    Task DeleteAsync(
        string? filePath,
        CancellationToken cancellationToken = default);
}