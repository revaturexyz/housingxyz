using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Revature.Account.DataAccess.Repositories;
using Revature.Account.Lib.Interface;
using Microsoft.EntityFrameworkCore;
using Revature.Account.DataAccess;
using Serilog;
using System.Security.Claims;

namespace Revature.Account.Api
{
  public class Startup
  {
    private const string ConnectionStringName = "AccountDb";
    private const string CorsPolicyName = "RevatureCorsPolicy";

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      Auth0Helper.SetSecretValues(Configuration.GetSection("Auth0").GetValue<string>("Domain"),
        Configuration.GetSection("Auth0").GetValue<string>("Audience"),
        Configuration.GetSection("Auth0").GetValue<string>("ClientId"),
        Configuration.GetSection("Auth0").GetValue<string>("ClientSecret"));

      services.AddControllers();
      services.AddDbContext<AccountDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("AccountDb")));

      services.AddScoped<IGenericRepository, GenericRepository>();

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

      services.AddScoped<IGenericRepository, GenericRepository>();
      services.AddTransient<IAuth0HelperFactory, Auth0HelperFactory>();

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(options =>
      {
        options.Authority = $"http://{Auth0Helper.Domain}/";
        options.Audience = Auth0Helper.Audience;
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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Revature Account V1");
      });

      app.UseRouting();

      app.UseCors(CorsPolicyName);

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      // Found at https://stackoverflow.com/questions/36958318/where-should-i-put-database-ensurecreated
      var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
      using (var serviceScope = serviceScopeFactory.CreateScope())
      {
        var dbContext = serviceScope.ServiceProvider.GetService<AccountDbContext>();
        dbContext.Database.EnsureCreated();
      }
    }
  }
}
