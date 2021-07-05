using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetCatalog.Application.Interfaces;
using PetCatalog.Application.Services;
using PetCatalog.Infra.Data.Context;
using PetCatalog.Infra.IoC;
using PetCatalog.MVC.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.MVC.Extensions
{
    public static class Configure
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

        public static bool UseSqlServer(this IConfiguration configuration) => configuration.GetValue<bool>("UseSqlServer");

        public static bool UseSqlite(this IConfiguration configuration) => configuration.GetValue<bool>("UseSqlite");

        public static void RegisterServices(this IServiceCollection services,string imagesSaveDir) => DependencyContainer.RegisterServices(services, imagesSaveDir);

        //Most be called after RegisterServices!!
        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            IMapperConfigurationExpression exs;
            services.RegisterModelMaps(out exs);
            exs.RegisterFormMaps(services.GetScopedService<ICategoryService>());
        }

    }
}
