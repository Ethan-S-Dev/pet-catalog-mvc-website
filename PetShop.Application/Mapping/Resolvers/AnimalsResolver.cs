using AutoMapper;
using PetShop.Application.ViewModels;
using PetShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.Mapping.Resolvers
{
    public class AnimalsResolver : IValueResolver<Category, CategoryViewModel, IEnumerable<AnimalViewModel>>
    {
        
        public IEnumerable<AnimalViewModel> Resolve(Category source, CategoryViewModel destination, IEnumerable<AnimalViewModel> destMember, ResolutionContext context)
        {
            var retList = new List<AnimalViewModel>(source.Animals.Count());
            foreach (var animal in source.Animals)
            {
                var aniVm = new AnimalViewModel()
                {
                    AnimalId = animal.AnimalId,
                    Name = animal.Name,
                    Age = animal.Age,
                    Description = animal.Description,
                    PictureName = animal.PictureName,
                    CategoryId = animal.CategoryId,
                    Category = destination
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

                retList.Add(aniVm);
            }

            return retList;
        }
    }
}
