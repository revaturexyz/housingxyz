using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Xyz.Provider.DataAccess.Entities;
using Xyz.Provider.DataAccess.Repository;
using Xyz.Provider.Lib.BusinessLogic;
using Xyz.Provider.Lib.Interface;

namespace Xyz.Provider.Api
{
  public class Startup
  {
    private const string ConnectionStringName = "ProviderDb";
    private const string CorsPolicyName = "RevatureCorsPolicy";

    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddHealthChecks()
        .AddDbContextCheck<RevatureHousingDbContext>();

      services.AddDbContext<RevatureHousingDbContext>(options =>
      {
        /* //Try something else
        if (Configuration.GetConnectionString(ConnectionStringName) is string connectionString)
        {
          options.UseSqlServer(connectionString, sqlOptions =>
          {
            sqlOptions.EnableRetryOnFailure(); // connection resiliency
          });
        }
        */

        //reference Secrets class instead
        if ( Secret.ProviderDb is string connectionString)
        {
          options.UseSqlServer(connectionString, sqlOptions =>
          {
            sqlOptions.EnableRetryOnFailure(); // connection resiliency
          });
        }
        else
        {
          throw new InvalidOperationException($"Connection string {ConnectionStringName} missing");
        }
      });

      services.AddScoped<IAddressRepository, AddressRepository>();
      services.AddScoped<IComplexRepository, ComplexRepository>();
      services.AddScoped<IGenderRepository, GenderRepository>();
      services.AddScoped<INotificationRepository, NotificationRepository>();
      services.AddScoped<IProviderRepository, ProviderRepository>();
      services.AddScoped<IRoomRepository, RoomRepository>();

      services.AddScoped<AddressLogic>();

      services.AddCors(options =>
      {
        options.AddPolicy(CorsPolicyName, builder =>
        {
          builder.WithOrigins("http://localhost:4200",
                              "https://localhost:4200",
                              "http://housing.revature.xyz",
                              "https://housing.revature.xyz",
                              "http://housingpre.revature.xyz",
                              "https://housingpre.revature.xyz")
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials();
        });
      });

      services.AddAuthentication(AzureADB2CDefaults.AuthenticationScheme)
        .AddAzureADB2C(options => Configuration.Bind(key: "AzureAdB2C", instance: options));

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Revature Housing", Version = "v1" });
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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Revature Housing V1");
      });

      app.UseRouting();

      app.UseCors(CorsPolicyName);

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapHealthChecks("/health");
        endpoints.MapControllers();
      });
    }
  }
}
