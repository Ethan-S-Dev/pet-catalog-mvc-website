using Microsoft.Extensions.DependencyInjection;
using PetShop.Application.Interfaces;
using PetShop.Application.Mapping;
using PetShop.Application.Mapping.Profiles;
using PetShop.Application.Mapping.Resolvers;
using PetShop.Application.Services;
using PetShop.Domain.Interfaces;
using PetShop.Infra.Data.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // PetShop.Application
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAnimalService, AnimalService>();

            // PetShop.Domain.Interfaces | PetShop.Infra.Data.Repositorys
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAnimalRepository, AnimalRepository>();       
            
        }
    }
}
