using AutoMapper;
using PetShop.Application.Mapping.Resolvers;
using PetShop.Application.ViewModels;
using PetShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.Mapping.Profiles
{
    public class AnimalProfile : Profile
    {
        public AnimalProfile()
        {
            

            CreateMap<AnimalViewModel, Animal>()
                .ForMember(dest => dest.AnimalId, ops => ops.MapFrom(src => src.AnimalId))
                .ForMember(dest => dest.Name, ops => ops.MapFrom(src => src.Name))
                .ForMember(dest => dest.PictureName, ops => ops.MapFrom(src => src.PictureName))
                .ForMember(dest => dest.CategoryId, ops => ops.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Age, ops => ops.MapFrom(src => src.Age))
                .ForMember(dest => dest.Description, ops => ops.MapFrom(src => src.Description));

            CreateMap<Animal, AnimalViewModel>()
                .ForMember(dest => dest.AnimalId, ops => ops.MapFrom(src => src.AnimalId))
                .ForMember(dest => dest.Name, ops => ops.MapFrom(src => src.Name))
                .ForMember(dest => dest.Age, ops => ops.MapFrom(src => src.Age))
                .ForMember(dest => dest.Description, ops => ops.MapFrom(src => src.Description))
                .ForMember(dest => dest.CategoryId, ops => ops.MapFrom(src => src.CategoryId))
                .ForMember(dest=>dest.Category,ops=>ops.MapFrom(new CategoryResolver()))
                .ForMember(dest=>dest.Comments,ops=>ops.MapFrom(new CommentsResolver()));               
                
        }
    }
}
