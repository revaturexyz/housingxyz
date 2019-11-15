using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Revature.Room.DataAccess;
using Revature.Room.DataAccess.Entities;
using Revature.Room.Lib;
using Serilog;
using ServiceBusMessaging;

namespace Revature.Room.Api
{
  public class Startup
  {
    private const string ConnectionStringName = "RoomDb";
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



      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Revature Room", Version = "v1" });
      });
      services.AddDbContext<RoomServiceContext>(options => options.UseNpgsql(Configuration.GetConnectionString("RoomDb")));

      services.AddScoped<IRepository, Repository>();
      services.AddScoped<IMapper, DBMapper>();

      services.AddScoped<ServiceBusSender>();
      services.AddSingleton<IServiceBusConsumer, ServiceBusConsumer>();

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
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "Revature Room V1");
      });

      var bus = app.ApplicationServices.GetService<IServiceBusConsumer>();
      bus.RegisterOnMessageHandlerAndReceiveMessages();

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
