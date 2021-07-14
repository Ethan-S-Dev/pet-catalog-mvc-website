using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PetCatalog.Domain.Auth;
using PetCatalog.Domain.Models;
using PetCatalog.Infra.Data.Contexts;
using PetCatalog.Infra.Data.DependencyInjections;
using PetCatalog.Infra.IoC;
using PetCatalog.MVC.Mappers;
using PetCatalog.MVC.Middlewares;
using System;
using System.IO;
using System.Text;

namespace PetCatalog.MVC.Extensions
{
    public static class ConfigureExtensions
    {
        public static void ConfigureSqlDb(this IServiceCollection services, IConfiguration configuration, bool useLazyLoading = true)
        {
            if (!configuration.IsValid()) throw new Exception(); // TODO: Add proper exception

            if (configuration.UseSqlServer())
            {
                services
                    .ConfigureForSqlServer(configuration
                    .GetConnectionString("SqlServerConnection"),
                    useLazyLoading);
            }

            if (configuration.UseSqlite())
            {
                services
                    .ConfigureForSqlite(configuration
                    .GetConnectionString("SqliteConnection"),
                    useLazyLoading);
            }
        }

        public static void ConfigureForSqlServer(this IServiceCollection services, string connectionString, bool useLazyLoading)
        {
            services.AddDbContext<PetCatalogDbContext>(options =>
                {
                    if (useLazyLoading)
                        options.UseLazyLoadingProxies();
                    options.UseSqlServer(connectionString);
                });
        }   

        public static void ConfigureForSqlite(this IServiceCollection services, string connectionString, bool useLazyLoading)
        {
            services.AddDbContext<PetCatalogDbContext>(options =>
                {
                    if (useLazyLoading)
                        options.UseLazyLoadingProxies();
                    options.UseSqlite(connectionString);
                });
        }

        public static bool IsValid(this IConfiguration configuration)
        {
            var isSqlite = configuration.UseSqlite();
            var isSqlServer = configuration.UseSqlServer();
            if (!(isSqlite ^ isSqlServer)) return false;

            return true;
        }

        public static bool UseSqlServer(this IConfiguration configuration) 
            => configuration.GetValue<bool>("UseSqlServer");

        public static bool UseSqlite(this IConfiguration configuration) 
            => configuration.GetValue<bool>("UseSqlite");

        public static void ConfigureFileSaver(this IServiceCollection services, IWebHostEnvironment webHostEnv, IConfiguration config) 
            => services.AddFileContext<ImageFileContext>(ops=>ops.UseSaveDir(Path.Combine(webHostEnv.WebRootPath,config.GetValue<string>("ImageDirectory"))));

        public static void RegisterServices(this IServiceCollection services)
            => DependencyContainer.RegisterServices(services);

        //Most be called after RegisterServices!!
        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.RegisterMaps();
            });
        }

        public static void RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IAuthorizationMiddlewareResultHandler, AuthorizeMiddlewareResultHandler>();

            var jwtSection = configuration.GetSection("JWTSettings");
            
            services.AddSession(ops => ops.IdleTimeout = TimeSpan.FromMinutes(configuration.GetValue<int>("SessionTimeOut")));

            services.Configure<JWTSettings>(jwtSection);

            var jwtSetings = jwtSection.Get<JWTSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSetings.SecretKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };                 
                });

        }

    }
}
