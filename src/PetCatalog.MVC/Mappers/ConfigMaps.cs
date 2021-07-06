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
            //mce.CreateMap<FormFile,>

            mce.CreateMap<AnimalViewModel, Animal>();
            mce.CreateMap<Animal, AnimalViewModel>();

            mce.CreateMap<CategoryViewModel, Category>();
            mce.CreateMap<Category, CategoryViewModel>();

            mce.CreateMap<Comment, CommentViewModel>();
            mce.CreateMap<CommentViewModel, Comment>();

            mce.CreateMap<ImageViewModel, Image>();
            mce.CreateMap<Image, ImageViewModel>();

            mce.CreateMap<IFormFile, ImageViewModel>()
                .ForMember(des => des.Name, cfg => cfg.MapFrom(src => src.FileName))
                .ForMember(des => des.data, cfg => cfg.ConvertUsing(new ImageConverter(),src=>src))
                .ForMember(des=>des.ImageId,cfg=>cfg.Ignore()); 

            mce.CreateMap<AnimalViewModel, AnimalFormModel>()
                .ForMember(dest => dest.Animal, cfg => cfg.MapFrom(src => src))
                .ForMember(dest => dest.CategoryId, cfg => cfg.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.CategoryName, cfg => cfg.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Categorys, cfg => cfg.Ignore()) // impotent
                .ForMember(dest => dest.Image, cfg => cfg.Ignore()); // ignore

            mce.CreateMap<AnimalFormModel, AnimalViewModel>()
                .ForMember(des => des.AnimalId, cfg => cfg.MapFrom(src => src.Animal.AnimalId))
                .ForMember(des => des.Name, cfg => cfg.MapFrom(src => src.Animal.Name))
                .ForMember(des => des.Age, cfg => cfg.MapFrom(src => src.Animal.Age))
                .ForMember(des => des.Description, cfg => cfg.MapFrom(src => src.Animal.Description))
                .ForMember(des => des.Image, cfg => cfg.MapFrom(src=>src.Image))
                .ForMember(des => des.CategoryId, cfg => cfg.Ignore()); // impotent

            mce.CreateMap<AnimalViewModel, AnimalEditModel>()
                .ForMember(dest => dest.Animal, cfg => cfg.MapFrom(src => src))
                .ForMember(dest => dest.CategoryId, cfg => cfg.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.CategoryName, cfg => cfg.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Categorys, cfg => cfg.Ignore()) // impotent
                .ForMember(dest => dest.Image, cfg => cfg.Ignore()); // ignore

            mce.CreateMap<AnimalEditModel, AnimalViewModel>()
                .ForMember(des => des.AnimalId, cfg => cfg.MapFrom(src => src.Animal.AnimalId))
                .ForMember(des => des.Name, cfg => cfg.MapFrom(src => src.Animal.Name))
                .ForMember(des => des.Age, cfg => cfg.MapFrom(src => src.Animal.Age))
                .ForMember(des => des.Description, cfg => cfg.MapFrom(src => src.Animal.Description))
                .ForMember(des => des.Image, cfg => cfg.MapFrom(src => src.Image))
                .ForMember(des => des.CategoryId, cfg => cfg.Ignore()); // impotent

            return mce;
        }

    }
}
