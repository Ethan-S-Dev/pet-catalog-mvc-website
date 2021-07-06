using Microsoft.Extensions.DependencyInjection;
using PetCatalog.Infra.Data.FileContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Infra.Data.DependencyInjections
{

    public static class FileContextServiceExtension
    {
        public static void AddFileContext<T>(this IServiceCollection services, Action<FileContextOptions> optionAction, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped) where T : FileContext
        {
            FileContext.Configuring = optionAction;
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton<T>();
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped<T>();
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient<T>();
                    break;
            }
        }

        public static void AddFileContext(this IServiceCollection services, Action<FileContextOptions> optionAction, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            FileContext.Configuring = optionAction;
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton<FileContext>();
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped<FileContext>();
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient<FileContext>();
                    break;
            }
        }
    }

}
