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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
                              "https://housingdev.revature.xyz")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
      });

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Revature Account", Version = "v1" });
        c.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");
        c.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
        {
          Type = SecuritySchemeType.ApiKey,
          Description = "Bearer authentication scheme with JWT, e.g. \"Bearer eyJhbGciOiJIUzI1NiJ9.e30\"",
          Name = "Authorization",
          In = ParameterLocation.Header
        });
        c.OperationFilter<SwaggerFilter>();
      });

      services.AddDbContext<ComplexDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("ComplexDb")));
      services.AddSingleton<IAuthorizationHandler, RoleRequirementHandler>();
      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(options =>
      {
        options.Authority = $"http://{Configuration.GetSection("Auth0").GetValue<string>("Domain")}/";
        options.Audience = Configuration.GetSection("Auth0").GetValue<string>("Audience");
      });

      services.AddAuthorization(options => {
        options.AddPolicy("ApprovedProviderRole", policy =>
            policy.Requirements.Add(new RoleRequirement("approved_provider")));
        options.AddPolicy("CoordinatorRole", policy =>
            policy.Requirements.Add(new RoleRequirement("coordinator")));
        // To fix needing to manually specify the schema every time I want to call [Authorize]
        // Found it at https://github.com/aspnet/AspNetCore/issues/2193
        options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build();
      });


      services.AddScoped<IRepository, Repository>();
      services.AddScoped<Mapper>();
      services.AddHostedService<RoomServiceReceiver>();
      services.AddScoped<IAddressService, AddressServiceSender>();
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
      app.UseAuthorization();
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
