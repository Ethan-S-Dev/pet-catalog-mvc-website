using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PetCatalog.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Infra.IoC
{
    public static class MapperDependency
    {
        public static IServiceCollection RegisterModelMaps(this IServiceCollection services,out IMapperConfigurationExpression configurationExpression)
        {
            // PetShop.Application
            // Auto Mapper                  
            IMapperConfigurationExpression ce = default;
            services.AddAutoMapper(cfg => { cfg.Configuration(); ce = cfg;  });
            configurationExpression = ce;
            return services;
        }


    }
}
