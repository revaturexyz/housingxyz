using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Revature.Address.DataAccess.Entities;
using Serilog;
using Serilog.Events;
using Revature.Address.DataAccess.Entities;

namespace Revature.Address.Api
{
  public static class Program
  {
    public static async Task Main(string[] args)
    {
      ConfigureLogger();

      try
      {
        Log.Information("Building web host");
        using var host = CreateHostBuilder(args).Build();
        await EnsureDatabaseCreatedAsync(host);

        Log.Information("Starting web host");
        await host.RunAsync();
        await EnsureDatabaseCreatedAsync(host);
      }
#pragma warning disable CA1031 // Do not catch general exception types
      catch (Exception ex)
      {
        Log.Fatal(ex, "Host terminated unexpectedly");
      }
#pragma warning restore CA1031 // Do not catch general exception types
      finally
      {
        Log.CloseAndFlush();
      }
    }

    public static void ConfigureLogger()
    {
      Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateLogger();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder.UseStartup<Startup>();
          webBuilder.UseSerilog();
        });

    public static async Task EnsureDatabaseCreatedAsync(IHost host)
    {
      using var scope = host.Services.CreateScope();
      var serviceProvider = scope.ServiceProvider;

      Log.Information("Ensuring database created");
      using var context = serviceProvider.GetRequiredService<AddressDbContext>();

      var created = await context.Database.EnsureCreatedAsync();
      if (created)
      {
        Log.Information("Database created");
      }
      else
      {
        Log.Information("Database already exists; not created");
      }
    }
  }
}
