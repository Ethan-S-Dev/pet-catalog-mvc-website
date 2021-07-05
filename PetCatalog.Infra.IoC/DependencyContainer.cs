﻿using Microsoft.Extensions.DependencyInjection;
using PetCatalog.Application.Interfaces;
using PetCatalog.Application.Mapping;
using PetCatalog.Application.Services;
using PetCatalog.Domain.Interfaces;
using PetCatalog.Infra.Data.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services,string imagesSaveLocation,string defaultName)
        {
            // PetShop.Application
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAnimalService, AnimalService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddSingleton<IImageService>(new ImageService(imagesSaveLocation, defaultName));

            // PetShop.Domain.Interfaces | PetShop.Infra.Data.Repositorys
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            
            
            
        }
    }
}