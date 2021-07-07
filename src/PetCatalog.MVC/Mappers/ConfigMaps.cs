using AutoMapper;
using Microsoft.AspNetCore.Http;
using PetCatalog.Application.Interfaces;
using PetCatalog.Domain.Models;
using PetCatalog.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.MVC.Mappers
{
    public static class ConfigMaps
    {

        public static IMapperConfigurationExpression RegisterMaps(this IMapperConfigurationExpression mce)
        {                 
            mce.CreateMap<CategoryViewModel, Category>();
            mce.CreateMap<Category, CategoryViewModel>();

            mce.CreateMap<Comment, CommentViewModel>();
            mce.CreateMap<CommentViewModel, Comment>();
            mce.CreateMap<IFormFile, Image>()
                .ConvertUsing(typeof(ImageConverter));

            mce.CreateMap<Animal, AnimalViewModel>()
                .ForMember(des=>des.CategoryName,cfg=>cfg.MapFrom(src=>src.Category.Name))
                .ForMember(des=>des.ImageName,cfg=>cfg.MapFrom(src=>src.Image.Name));

            mce.CreateMap<AnimalViewModel, Animal>()
                .ForPath(des=>des.Image.Name,cfg=>cfg.MapFrom(src=>src.ImageName));

            mce.CreateMap<IEnumerable<Animal>, IEnumerable<AnimalViewModel>>();

            return mce;
        }

    }
}
