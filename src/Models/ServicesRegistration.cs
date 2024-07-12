namespace TestProject.WebApi.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using TestProject.Context;
using TestProject.Repositories;

public static class ServicesRegistration
{
    public static void AddServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Add Application Db Context options
        services.AddDbContextPool<ModelDBContext>(options =>
            options
            .UseNpgsql(connectionString).LogTo(Log.Logger.Information, LogLevel.Information, null)
        );

        //Add Repositories
        services.AddScoped<IFileRepository, FileRepository>();
        services.AddScoped<IFileManagementService, FileManagementService>();
    }
}