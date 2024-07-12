using Microsoft.EntityFrameworkCore;
using Serilog;
using TestProject.Domain.Entities;

namespace TestProject.Context;

public class ModelDBContext : DbContext
{
    public ModelDBContext(DbContextOptions<ModelDBContext> options) : base(options)
    {

    }
    static ModelDBContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
    }

    public override void Dispose()
    {
        base.Dispose();
        Log.Debug("***ModelDBContext Disposed");
    }

    public override ValueTask DisposeAsync()
    {
        Log.Debug("***ModelDBContext DisposeAsync");
        return base.DisposeAsync();
    }

    public DbSet<FileModel> FileModel { get; set; } = null!;
}