using Serilog;
using TestProject.Domain.Entities;
using TestProject.Repositories;

namespace TestProject.WebApi.Services;

public class FileManagementService : IFileManagementService
{
    const string FileDirectory = "Uploads";

    private readonly IFileRepository _fileRepository;


    public FileManagementService(
        IFileRepository fileRepository
    )
    {
        _fileRepository = fileRepository;
    }

    public async Task<FileStream?> GetFileAsync(Guid fileId)
    {
        try
        {
            // check if the file exists
            if (await FileExistsAsync(fileId))
            {
                var path = await GetFilePath(fileId);
                return File.OpenRead(path);
            }
            else
            {
                return null;
            }
        }
        catch
        {
            Log.Error("Error getting file {fileId}", fileId);
            return null;
        }
    }

    public async Task<FileModel?> SaveFileAsync(IFormFile uploadingFile)
    {
        if (uploadingFile == null || uploadingFile.Length == 0)
            return null;

        var fileId = Guid.NewGuid();
        var fileExtension = Path.GetExtension(uploadingFile.FileName);
        var fileName = Path.GetFileNameWithoutExtension(uploadingFile.FileName);
        var filePath = MakeFilePath(fileId, fileExtension);

        var file = new FileModel
        {
            Id = fileId,
            FileExtension = fileExtension,
            FileName = fileName,
            FilePath = filePath,
            FileSize = uploadingFile.Length
        };

        await _fileRepository.Create(file);

        using (var fileStream = File.Create(filePath))
        {
            await uploadingFile.CopyToAsync(fileStream);
        }

        return file;
    }

    public async Task<bool> DeleteFileAsync(Guid fileId)
    {
        try
        {
            // check if the file exists
            if (FileExistsAsync(fileId).Result == false)
            {
                return false;
            }

            // delete the file
            var filePath = await GetFilePath(fileId);
            File.Delete(filePath);

            return true;
        }
        catch
        {
            Log.Error("Error deleting file {fileId}", fileId);
            return false;
        }
    }

    public async Task<bool> FileExistsAsync(Guid fileId)
    {
        try
        {
            var file = await _fileRepository.GetById(fileId);
            if (file == null)
            {
                return false;
            }
            else
            {
                var path = await GetFilePath(fileId);
                return File.Exists(path);
            }
        }
        catch
        {
            Log.Error("Error checking if file exists {fileId}", fileId);
            return false;
        }
    }

    // Utils

    private async Task<string> GetFilePath(Guid fileId)
    {
        var file = await _fileRepository.GetById(fileId);

        if (file == null)
        {
            throw new Exception("File not found");
        }

        return MakeFilePath(fileId, file.FileExtension);
    }
    private static string MakeFilePath(Guid fileId, string fileExtension)
    {
        return Path.Combine(Directory.GetCurrentDirectory(), FileDirectory, $"{fileId}{fileExtension}");
    }

}