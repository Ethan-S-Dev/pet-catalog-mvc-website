using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetCatalog.Application.Interfaces;
using PetCatalog.Application.Mapping;
using PetCatalog.Application.Services;
using PetCatalog.Infra.Data.Context;
using PetCatalog.Infra.IoC;
using PetCatalog.MVC.Mappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.MVC.Extensions
{
    public static class ConfigureExtensions
    {
        public static void ConfigureSqlDb(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration, bool useLazyLoading = true)
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

        public static bool IsValid(this Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            var isSqlite = configuration.UseSqlite();
            var isSqlServer = configuration.UseSqlServer();
            if (!(isSqlite ^ isSqlServer)) return false;

            return true;
        }

        public static bool UseSqlServer(this Microsoft.Extensions.Configuration.IConfiguration configuration) => configuration.GetValue<bool>("UseSqlServer");

        public static bool UseSqlite(this Microsoft.Extensions.Configuration.IConfiguration configuration) => configuration.GetValue<bool>("UseSqlite");

        public static void RegisterServices(this IServiceCollection services, IWebHostEnvironment webHostEnv, Microsoft.Extensions.Configuration.IConfiguration config)
            => 
            DependencyContainer.RegisterServices(services, Path.Combine(webHostEnv.WebRootPath,"res\\images\\animals\\"), config.GetValue<string>("DefaultImageName"));

        //Most be called after RegisterServices!!
        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.Configuration();
                cfg.RegisterFormMaps();
            });
        }

    }
}
