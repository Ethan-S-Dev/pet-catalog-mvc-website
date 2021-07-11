using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetCatalog.Infra.Data.Contexts;
using PetCatalog.MVC.Extensions;
using System.Text.Json;
using System;
using System.Net;
using PetCatalog.Application.Auth;
using System.IO;
using System.Diagnostics;
using System.Text;

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

            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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

            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("accessToken");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            });

            //app.Use(async (context, next) =>
            //{
            //    string token;
            //    if (context.Request.Cookies.TryGetValue("refreshToken",out token))
            //    {                 
            //        context.Request.Headers.Add("Authorization", "Bearer " + token);
            //    }
            //    await next();
            //});


            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.Use(async (context,next) =>
            {
                var response = context.Response;

                if (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                    response.StatusCode == (int)HttpStatusCode.Forbidden)
                {

                    string refreshToken;
                    if(context.Request.Cookies.TryGetValue("refreshToken",out refreshToken))
                    {
                        var accessToken = context.Session.GetString("accessToken");
                        if (accessToken is not null)
                        if(refreshToken is not null)
                        {
                            var body = JsonSerializer.Serialize(new RefreshRequest() { RefreshToken = refreshToken, AccessToken = accessToken });
                                try
                                {
                                    var buffer = Encoding.UTF8.GetBytes(body);
                                    using var bodyStream = context.Request.Body;
                                    bodyStream.Position = 0;
                                    bodyStream.Write(buffer, 0, buffer.Length);
                                }catch(Exception e)
                                {
                                    Debug.WriteLine(e.Message);
                                }

                                                    


                            response.Redirect("/login/RefreshToken");                            
                        }
                    }
                    response.Redirect("/login");
                }
                   
            });
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
