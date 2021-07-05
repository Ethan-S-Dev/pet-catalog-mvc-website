using AutoMapper;
using Microsoft.AspNetCore.Http;
using PetCatalog.Application.Interfaces;
using PetCatalog.Application.ViewModels;
using PetCatalog.MVC.Models;
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

        public static IMapperConfigurationExpression RegisterFormMaps(this IMapperConfigurationExpression mce, ICategoryService categoryService, IImageService imageService)
        {
            //mce.CreateMap<FormFile,>

            mce.CreateMap<AnimalViewModel, AnimalFormModel>()
                .ForMember(dest => dest.Animal, cfg => cfg.MapFrom(src => src))
                .ForMember(dest => dest.CategoryId, cfg => cfg.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.CategoryName, cfg => cfg.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Categorys, cfg => cfg.MapFrom(src => categoryService.GetCategorys()))
                .ForMember(dest => dest.Image, cfg => cfg.Ignore());

            mce.CreateMap<AnimalFormModel, AnimalViewModel>()
                .ForMember(des => des.AnimalId, cfg => cfg.MapFrom(src => src.Animal.AnimalId))
                .ForMember(des => des.Name, cfg => cfg.MapFrom(src => src.Animal.Name))
                .ForMember(des => des.Age, cfg => cfg.MapFrom(src => src.Animal.Age))
                .ForMember(des => des.PictureName, cfg => cfg.MapFrom(src => CreateImage(src, imageService)))
                .ForMember(des => des.Description, cfg => cfg.MapFrom(src => src.Animal.Description))
                .ForMember(des => des.CategoryId, cfg => cfg.MapFrom(src => CreateOrUseCategory(src, categoryService)));

            mce.CreateMap<AnimalViewModel, AnimalEditModel>()
                .ForMember(dest => dest.Animal, cfg => cfg.MapFrom(src => src))
                .ForMember(dest => dest.CategoryId, cfg => cfg.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.CategoryName, cfg => cfg.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Categorys, cfg => cfg.MapFrom(src => categoryService.GetCategorys()))
                .ForMember(dest => dest.Image, cfg => cfg.Ignore()); ;
            mce.CreateMap<AnimalEditModel, AnimalViewModel>()
                .ForMember(des => des.AnimalId, cfg => cfg.MapFrom(src => src.Animal.AnimalId))
                .ForMember(des => des.Name, cfg => cfg.MapFrom(src => src.Animal.Name))
                .ForMember(des => des.Age, cfg => cfg.MapFrom(src => src.Animal.Age))
                .ForMember(des => des.PictureName, cfg => cfg.MapFrom(src => UpdateImage(src, imageService)))
                .ForMember(des => des.Description, cfg => cfg.MapFrom(src => src.Animal.Description))
                .ForMember(des => des.CategoryId, cfg => cfg.MapFrom(src => CreateOrUseCategory(src, categoryService))); ;

            return mce;
        }

        static string CreateImage(AnimalFormModel src, IImageService imageService)
        {
            if (src.Image is not null)
            {
                var unsafeFileName = Path.GetFileName(src.Image.FileName);
                var exten = Path.GetExtension(unsafeFileName);
                var newFileName = $"{Guid.NewGuid()}{exten}";
                using var imgStream = new MemoryStream();
                src.Image.CopyTo(imgStream);
                if (imageService.SaveImage(newFileName, imgStream))
                {
                    return newFileName;
                }
            }

            return imageService.DefaultName;
        }
        static string UpdateImage(AnimalEditModel src, IImageService imageService)
        {
            if (src.Image is not null)
            {
                var unsafeFileName = Path.GetFileName(src.Image.FileName);
                var exten = Path.GetExtension(unsafeFileName);
                var newFileName = $"{Guid.NewGuid()}{exten}";
                var oldFileName = src.Animal.PictureName;
                using var imgStream = new MemoryStream();
                src.Image.CopyTo(imgStream);
                if (imageService.UpdateImage(oldFileName, newFileName, imgStream))
                {
                    return newFileName;
                }
            }

            return oldFileName;
        }

        static int CreateOrUseCategory(AnimalFormModel src, ICategoryService categoryService)
        {
            var cate = categoryService.GetCategory(src.CategoryId);
            if (cate is not null) return src.CategoryId;

            categoryService.AddCategory(src.CategoryName, out int id);
            return id;
        }

        static int CreateOrUseCategory(AnimalEditModel src, ICategoryService categoryService)
        {
            var cate = categoryService.GetCategory(src.CategoryId);
            if (cate is not null) return src.CategoryId;

            categoryService.AddCategory(src.CategoryName, out int id);
            return id;
        }

    }
}
