
using TestProject.Domain.Entities;

namespace TestProject.Repositories;

public interface IFileRepository
{
    Task<FileModel?> GetById(Guid id);
    Task<FileModel> Create(FileModel file);
    Task<FileModel> Update(FileModel file);
    Task Delete(Guid id);
    Task<IEnumerable<FileModel>> GetAll();
    Task<IEnumerable<FileModel>> GetByFileName(string fileName);
}