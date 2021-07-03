using AutoMapper;
using PetShop.Application.Interfaces;
using PetShop.Application.ViewModels;
using PetShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.Mapping.Resolvers
{
    public class CategoryResolver : IValueResolver<Animal, AnimalViewModel, CategoryViewModel>
    {
        public CategoryViewModel Resolve(Animal source, AnimalViewModel destination, CategoryViewModel destMember, ResolutionContext context)
        {
            var aniViewList = new List<AnimalViewModel>(source.Category.Animals.Count());
            foreach (var animal in source.Category.Animals)
            {
                var aniVm = new AnimalViewModel()
                {
                    AnimalId = animal.AnimalId,
                    Name = animal.Name,
                    Age = animal.Age,
                    Description = animal.Description,
                    PictureName = animal.PictureName,
                    CategoryId = animal.CategoryId,
                    Category = destMember
                };

                var commentsList = new List<CommentViewModel>(animal.Comments.Count());               
                foreach (var comm in animal.Comments)
                {
                    commentsList.Add(new CommentViewModel()
                    {
                        CommentId = comm.CommentId,
                        AnimalId = comm.AnimalId,
                        Value = comm.Value,
                        Animal = aniVm
                    });
                }
                aniVm.Comments = commentsList;

                aniViewList.Add(aniVm);
            }
            var cateVm = new CategoryViewModel()
            {
                CategoryId = source.CategoryId,
                Name = source.Category.Name,
                Animals = aniViewList
            };

            return cateVm;
        }
    }
}
