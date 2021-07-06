using PetCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Application.Interfaces
{
    public interface ICommentService
    {
        IEnumerable<Comment> GetCommensts();
        Comment GetComment(int commentId);
        IEnumerable<Comment> GetComments(int animalId);
        void AddComment(Comment comment);
        void DeleteComment(int id);

        void DeleteComments(int animalId);
    }
}
