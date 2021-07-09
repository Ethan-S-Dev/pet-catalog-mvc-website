using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetCatalog.Infra.Data.Contexts;
using PetCatalog.MVC.Extensions;


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
       
        public void Configure(IApplicationBuilder app, PetCatalogDbContext ctx,ImageFileContext fs)
        {
            //ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            //fs.Diractory.EnsureDeleted();
            fs.Diractory.EnsureCreated();


            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            //app.Run(async c =>
            //{
            //     c.Response.Redirect("/");
            //});
        }

        
    }
}
