using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xyz.Address.Dbx;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Xyz.Address.Dbx.Repositories;
using Swashbuckle.AspNetCore.Swagger;


namespace Xyz.Address.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
          Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {   
            services.AddMvc(options =>
            {
              options.Filters.Add(new GlobalExceptionHandlerAttribute());
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            var ConnectionString = Configuration["ConnectionStrings"];

            services.AddSwaggerGen(document => 
            {
              document.SwaggerDoc ("v1", new Info
              {
                Title = "Address API Documentation",
                Version = "v1",
                Description = "Interactive API documentation that lets your users try out the Address API calls",
                TermsOfService = "https://raw.githubusercontent.com/revaturexyz/addressxyz/master/LICENSE.txt",
                Contact = new Contact {Name = "Fred Belotte", Email = "fred@revature.com", Url = "https://github.com/revaturexyz/addressxyz"},
                License = new License {Name = "Licensed under MIT", Url = "https://raw.githubusercontent.com/revaturexyz/addressxyz/master/LICENSE.txt"}

              });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                document.IncludeXmlComments(xmlPath);
            });


            services.AddEntityFrameworkNpgsql().AddDbContext<AddressDbContext>(options => options.UseNpgsql(ConnectionString, npgsql =>{
              npgsql.EnableRetryOnFailure(); //we will continue to attempt to connect to the database
            }));             
          
            services.AddScoped<AddressDbContext>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AddressDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using (var scope =  app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope()){
              scope.ServiceProvider.GetService<AddressDbContext>().Database.Migrate();
            }
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(config => 
            {
              config.SwaggerEndpoint("/swagger/v1/swagger.json", "Address API Documentation");
            }
            );
        }
    }
}
