using PetCatalog.Application.Interfaces;
using PetCatalog.Domain.Interfaces;
using PetCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }
        public void AddComment(Comment comment)
        {
            commentRepository.Create(comment);
        }

        public void DeleteComment(int id)
        {
            commentRepository.Delete(id);
        }

        public void DeleteComments(int animalId)
        {
            commentRepository.Delete(animalId);
        }

        public IEnumerable<Comment> GetCommensts()
        {                   
            return commentRepository.GetAll().ToList();
        }

        public Comment GetComment(int commentId)
        {
            return commentRepository.Get(commentId);
        }

        public IEnumerable<Comment> GetComments(int animalId)
        {
            return commentRepository.GetAnimalComments(animalId).ToList();
        }

    }
}
