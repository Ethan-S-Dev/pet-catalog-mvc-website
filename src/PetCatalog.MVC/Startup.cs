using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetCatalog.Infra.Data.Contexts;
using PetCatalog.MVC.Extensions;
using System.IO;

namespace PetCatalog.MVC
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment webHostEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            this.webHostEnvironment = webHostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.ConfigureSqlDb(configuration);           

            services.ConfigureFileSaver(webHostEnvironment, configuration);

            services.RegisterAuthentication(configuration);

            services.RegisterServices();

            services.RegisterAutoMapper();
        }

        public void Configure(IApplicationBuilder app, PetCatalogDbContext ctx, ImageFileContext fs)
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            fs.Diractory.EnsureDeleted();
            fs.Diractory.EnsureCreated();

            app.UseSession();

            app.UseAddAuthorization();

            app.UseAddAuthorization();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.Run(async c =>
            {
                await c.Response.WriteAsync("Error!");
            });
        }
    }
}
