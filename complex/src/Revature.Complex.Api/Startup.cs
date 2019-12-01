using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using Revature.Complex.Lib.Interface;
using Revature.Complex.Api.Services;
using Revature.Complex.DataAccess.Repository;
using Revature.Complex.DataAccess;
using Revature.Complex.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Revature.Complex.Api
{
  public class Startup
  {
    private const string ConnectionStringName = "ComplexDb";
    private const string CorsPolicyName = "RevatureCorsPolicy";

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    /// <summary>
    /// to configure the services
    /// </summary>
    /// <param name="services"></param>
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
                              "https://housingdev.revature.xyz",
                              "http://192.168.99.100:10080",
                              "https://192.168.99.100:10080",
                              "http://192.168.99.100:13080",
                              "https://192.168.99.100:13080")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
      });

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Revature Complex", Version = "v1" });
      });

      services.AddDbContext<ComplexDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("ComplexDb")));

      services.AddScoped<IRepository, Repository>();
      services.AddScoped<Mapper>();
      services.AddHostedService<RoomServiceReceiver>();
      services.AddScoped<IRoomServiceSender, RoomServiceSender>();

      services.AddControllers();

    }

    /// <summary>
    /// it is to create the app's request processing pipeline
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
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
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "Revature Complex V1");
      });

      app.UseRouting();

      app.UseCors(CorsPolicyName);

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      ////for the service-bus listener
      ////define the event-listener
      //var bus = app.ApplicationServices.GetService<IRoomServiceReceiver>();

      ////start listening
      //bus.RegisterOnMessageHandlerAndReceiveMessages();
    }
  }
}
