using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Revature.Address.DataAccess.Entities;
using Revature.Address.DataAccess.Interfaces;
using Revature.Address.Lib.BusinessLogic;
using Revature.Address.Lib.Interfaces;
using Serilog;

namespace Revature.Address.Api
{
  /// <summary>
  /// Handles the startup configuration for the
  /// address api
  /// </summary>
  public class Startup
  {
    private const string CorsPolicyName = "RevatureCorsPolicy";

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors(options =>
      {
        options.AddPolicy(CorsPolicyName, builder =>
        {
          builder.WithOrigins("http://localhost:4200",
                              "https://localhost:4200",
                              "http://housing.revature.xyz",
                              "https://housing.revature.xyz",
                              "http://housingdev.revature.xyz",
                              "https://housingdev.revature.xyz")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
      });
      services.AddDbContext<AddressDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("AddressDb")));

      services.AddScoped<IMapper, DataAccess.Mapper>();
      services.AddScoped<IDataAccess, DataAccess.DataAccess>();
      services.AddScoped<IAddressLogic, AddressLogic>();

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Revature Address", Version = "v1" });
      });

      services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseSerilogRequestLogging();

      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "Revature Address V1");
      });

      app.UseRouting();

      app.UseCors(CorsPolicyName);

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
