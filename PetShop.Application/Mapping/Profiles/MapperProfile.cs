using AutoMapper;
using PetShop.Application.ViewModels;
using PetShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.Mapping.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Comment, CommentViewModel>();
            CreateMap<Animal, AnimalViewModel>();
            CreateMap<Category, CategoryViewModel>();
        }
    }
}
