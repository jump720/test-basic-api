namespace TestProject.WebApi.Utilities;

using System.Reflection;
using Microsoft.Extensions.Configuration;
using Serilog;

public static class SerilogSupport
{
    public static void AddSerilogSupport(this ConfigureHostBuilder host, ConfigurationManager configuration)
    {
        host.UseSerilog((context, config) =>
                          config.ReadFrom.Configuration(context.Configuration)
                          .Enrich.WithProperty("appcode", Assembly.GetExecutingAssembly().GetName().Name)
                          .Enrich.WithProperty("appversion", configuration.GetSection("ApiVersion").Value)
                      );
    }
}