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
    public class CommentsResolver : IValueResolver<Animal, AnimalViewModel, IEnumerable<CommentViewModel>>
    {
        public IEnumerable<CommentViewModel> Resolve(Animal source, AnimalViewModel destination, IEnumerable<CommentViewModel> destMember, ResolutionContext context)
        {
            var retCommentList = new List<CommentViewModel>(source.Comments.Count());
            foreach (var comment in source.Comments)
            {
                retCommentList.Add(new CommentViewModel()
                {
                    CommentId = comment.CommentId,
                    AnimalId = comment.AnimalId,
                    Value = comment.Value,
                    Animal = destination
                });
            }

            return retCommentList;
        }
    }
}
