using Microsoft.Extensions.DependencyInjection;
using PetCatalog.Infra.Data.FileSavers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Infra.Data.DependencyInjections
{

    public static class FileSaverServiceExtension
    {
        public static void AddFileSaver(this IServiceCollection services, Action<FileSaverOptions> optionAction, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            FileSaver.Configuring = optionAction;
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton<FileSaver>();
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped<FileSaver>();
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient<FileSaver>();
                    break;
            }
        }
    }

}
