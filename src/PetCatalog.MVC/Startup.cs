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
using PetCatalog.Application.Interfaces;

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

        public void Configure(IApplicationBuilder app, IAuthService authService, PetCatalogDbContext ctx, ImageFileContext fs)
        {
            //ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            //fs.Diractory.EnsureDeleted();
            fs.Diractory.EnsureCreated();

            app.Use(async (context, next) =>
            {
                string token;
                if (context.Request.Cookies.TryGetValue("accessToken", out token))
                {
                    if (!string.IsNullOrEmpty(token))
                    {
                        context.Request.Headers.Add("Authorization", "Bearer " + token);
                    }
                }
                await next();
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;
                var httpContext = context.HttpContext;
                if (response.StatusCode == (int)HttpStatusCode.Unauthorized || response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    string refreshToken;

                    if (!httpContext.Request.Cookies.TryGetValue("refreshToken", out refreshToken))
                        response.Redirect($"/login?path={httpContext.Request.Path}");
                    else
                    {
                        string accessToken;
                        if(!httpContext.Request.Cookies.TryGetValue("accessToken", out accessToken))
                            response.Redirect($"/login?path={httpContext.Request.Path}");
                        else
                        {
                            if (refreshToken is null || accessToken is null)                           
                                response.Redirect($"/login?path={httpContext.Request.Path}");                            
                            else
                            {
                                var refreshRequest = new RefreshRequest() { RefreshToken = refreshToken, AccessToken = accessToken };
                                var userWithTokens = authService.RefreshToken(refreshRequest);

                                httpContext.Response.Cookies.Delete("accessToken");
                                httpContext.Response.Cookies.Append("accessToken", userWithTokens.AccessToken);

                                response.Redirect(httpContext.Request.Path);
                            }
                        }                   
                    }
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
