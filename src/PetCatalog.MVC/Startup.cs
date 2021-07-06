using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetCatalog.Infra.Data.Context;
using PetCatalog.MVC.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PetCatalog.MVC
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment webHostEnvironment;

        public Startup(IConfiguration configuration,IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            this.webHostEnvironment = webHostEnvironment;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.ConfigureSqlDb(configuration);

            services.CunfigureFileSaver(webHostEnvironment, configuration);

            services.RegisterServices();
          
            services.RegisterAutoMapper();
        }
       
        public void Configure(IApplicationBuilder app, PetCatalogDbContext ctx)
        {
            ctx.Database.EnsureCreated();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.Run(async c =>
            {
                 c.Response.Redirect("/");
            });
        }

        
    }
}
