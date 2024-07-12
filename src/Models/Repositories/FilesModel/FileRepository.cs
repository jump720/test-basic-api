using Microsoft.EntityFrameworkCore;
using TestProject.Context;
using TestProject.Domain.Entities;

namespace TestProject.Repositories;

public class FileRepository : IFileRepository
{
    protected readonly ModelDBContext Context;
    public FileRepository(ModelDBContext context)
    {
        Context = context;
    }

    public Task<FileModel?> GetById(Guid id)
    {
        return Context.FileModel.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<FileModel> Create(FileModel file)
    {
        Context.FileModel.Add(file);
        await Context.SaveChangesAsync();
        return file;
    }

    public async Task<FileModel> Update(FileModel file)
    {
        Context.FileModel.Update(file);
        await Context.SaveChangesAsync();
        return file;
    }

    public async Task Delete(Guid id)
    {
        var file = await GetById(id);
        if (file != null)
        {
            Context.FileModel.Remove(file);
            await Context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<FileModel>> GetAll()
    {
        return await Context.FileModel.ToListAsync();
    }

    public async Task<IEnumerable<FileModel>> GetByFileName(string fileName)
    {
        return await Context.FileModel.Where(x => x.FileName == fileName).ToListAsync();
    }
}