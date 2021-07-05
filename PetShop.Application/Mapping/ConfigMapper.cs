using AutoMapper;
using PetCatalog.Application.ViewModels;
using PetCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Application.Mapping
{
    public static class ConfigMapper
    {
        public static IMapperConfigurationExpression Configuration(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Comment, CommentViewModel>();
            cfg.CreateMap<CommentViewModel, Comment>();

            cfg.CreateMap<Animal, AnimalViewModel>();
            cfg.CreateMap<AnimalViewModel, Animal>();

            cfg.CreateMap<Category, CategoryViewModel>();
            cfg.CreateMap<CategoryViewModel, Category>();

            return cfg;
        }
        
    }
}
