using AutoMapper;
using PetShop.Application.Interfaces;
using PetShop.Application.ViewModels;
using PetShop.Domain.Interfaces;
using PetShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;
        private readonly IMapper mapper;
        public CommentService(ICommentRepository commentRepository,IMapper mapper)
        {
            this.commentRepository = commentRepository;
            this.mapper = mapper;
        }
        public void AddComment(CommentViewModel comment)
        {
            var realComment = mapper.Map<Comment>(comment);
            commentRepository.AddComment(realComment);
        }

        public void DeleteComment(int id)
        {
            commentRepository.DeleteComment(id);
        }
    
        public IEnumerable<CommentViewModel> GetCommensts()
        {
            var comments = commentRepository.GetAllComments();
            var list = new List<CommentViewModel>(comments.Count());
            foreach (var com in comments)
                list.Add(mapper.Map<CommentViewModel>(com));
            return list;
        }

        public CommentViewModel GetComment(int commentId)
        {
            var comment = commentRepository.GetComment(commentId);
            if (comment is null) return null;
            var commentDto = mapper.Map<CommentViewModel>(comment);
            return commentDto;
        }

        public IEnumerable<CommentViewModel> GetComments(int animalId)
        {
            var comments = commentRepository.GetAnimalComments(animalId);
            var list = new List<CommentViewModel>(comments.Count());
            foreach (var com in comments)
                list.Add(mapper.Map<CommentViewModel>(com));
            return list;
        }

    }
}
