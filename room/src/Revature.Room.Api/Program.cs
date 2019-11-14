using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Revature.Room.Api
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

        Log.Information("Starting web host");
        await host.RunAsync();
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
  }
}
