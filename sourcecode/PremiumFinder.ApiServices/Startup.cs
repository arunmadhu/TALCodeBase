using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PremiumDomain.Infrastructure;
using PremiumDomain.Services;
using PremiumFinder.ApiServices.Logging;
using PremiumFinder.ApiServices.MiddleWare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremiumFinder.ApiServices
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
            services.AddControllers();

            services.AddDbContext<PremiumDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PremiumDB")));

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("uiPortal", policy =>
                {
                    policy.WithOrigins(Configuration.GetSection("CorsSettings")["UIPortalUrl"].ToString())
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddBussinessLogic();

            services.AddAutoMapper(typeof(Startup));

            //services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            Serilog.Log.Logger = Logger.ConfigureLogger(Configuration, app);

            app.UseMiddleware<GlobalExceptionHandler>(env.IsDevelopment());

            app.UseCors("uiPortal");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseDefaultFiles(new DefaultFilesOptions
            {
                DefaultFileNames = new
                    List<string> { "index.html" }
            });
        }
    }
}
