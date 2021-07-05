using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.MVC.Extensions
{
    public static class Services
    {
        public static T GetScopedService<T>(this IServiceCollection services)
        {
            using var provider = services.BuildServiceProvider();
            return provider.GetService<T>();
        }
    }
}
