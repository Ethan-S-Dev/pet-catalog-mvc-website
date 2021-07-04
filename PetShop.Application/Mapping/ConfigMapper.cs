using AutoMapper;
using PetShop.Application.ViewModels;
using PetShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.Mapping
{
    public class ConfigMapper
    {
        public static Action<IMapperConfigurationExpression> Configuration => cfg=>
        {
            cfg.CreateMap<Comment, CommentViewModel>();
            cfg.CreateMap<Animal, AnimalViewModel>();
            cfg.CreateMap<Category, CategoryViewModel>();
            cfg.CreateMap<CommentViewModel, Comment>();
        };
        
    }
}
