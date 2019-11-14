using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ComplexService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(MyAllowSpecificOrigins,
            //    builder =>
            //    {
            //        builder.WithOrigins("http://localhost:4200",
            //                            "https://sweentened-angular.azurewebsites.net")
            //                                //Might need to add the app service website from microsoft azure site here in WithOrigins

            //                                //Added this to allow any method or header in angular to prevent errors
            //                                .AllowAnyHeader()
            //                                .AllowAnyMethod()
            //                                .AllowCredentials();
            //    });
            //});


            string connectionString = Configuration.GetConnectionString("complex");

            // among the services you register for DI (dependency injection)
            // should be your DbContext.
            //services.AddDbContext<SWTDbContext>(options =>
            //{
            //    options.UseNpgsql(connectionString);
            //});
            //services.AddControllers();

            //services.AddScoped<IRepo, Repo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
