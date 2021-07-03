using Microsoft.Extensions.DependencyInjection;
using PetShop.Application.Mapping;
using PetShop.Application.Mapping.Profiles;
using PetShop.Application.Mapping.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Infra.IoC
{
    public class MapperDependency
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // PetShop.Application
            // Auto Mapper           
            services.AddAutoMapper(ConfigMapper.Configuration);
        }


    }
}
