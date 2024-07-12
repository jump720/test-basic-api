
using TestProject.Domain.Entities;

namespace TestProject.WebApi.Services;
public interface IFileManagementService
{
    Task<FileStream?> GetFileAsync(Guid fileId);
    Task<FileModel?> SaveFileAsync(IFormFile uploadingFile);
    Task<bool> DeleteFileAsync(Guid fileId);
    Task<bool> FileExistsAsync(Guid fileId);
}