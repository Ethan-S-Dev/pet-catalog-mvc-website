using PetShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.Interfaces
{
    public interface ICommentRepository
    {
        // Create
        public void AddComment(Comment comment);

        // Read
        public IEnumerable<Comment> GetAllComments();
        public IEnumerable<Comment> GetAnimalComments (int animalId);
        public Comment GetComment(int commentId);
        // Update
        // Delete
        public Comment DeleteComment(int commentId);
        IEnumerable<Comment> DeleteComments(int animalId);
    }
}
