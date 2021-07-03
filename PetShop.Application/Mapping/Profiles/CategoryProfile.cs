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
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryViewModel>()
                .ForMember(dest => dest.CategoryId, ops => ops.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Name, ops => ops.MapFrom(src => src.Name))
                .ForMember(dest => dest.Animals, ops => ops.MapFrom(new AnimalsResolver()));
        }
    }
}
