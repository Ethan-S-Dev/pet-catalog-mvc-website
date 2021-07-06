using PetCatalog.Domain.Interfaces;
using PetCatalog.Domain.Models;
using PetCatalog.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Infra.Data.Repositorys
{
    public class CommentRepository : ICommentRepository
    {
        private readonly PetCatalogDbContext dbContext;
        public CommentRepository(PetCatalogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void AddComment(Comment comment)
        {
            dbContext.Comments.Add(comment);
            dbContext.SaveChanges();
        }

        public Comment DeleteComment(int commentId)
        {
            var toRemove = GetComment(commentId);
            if (toRemove is null) return null;
            dbContext.Comments.Remove(toRemove);
            dbContext.SaveChanges();
            return toRemove;
        }

        public IEnumerable<Comment> DeleteComments(int animalId)
        {
            var toRemove = GetAnimalComments(animalId);
            if (toRemove.Count() == 0) return null;
            foreach (var comm in toRemove)
            {
                dbContext.Comments.Remove(comm);
            }
            dbContext.SaveChanges();
            return toRemove;
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return dbContext.Comments;
        }

        public IEnumerable<Comment> GetAnimalComments(int animalId)
        {
            return dbContext.Comments.Where(c => c.AnimalId == animalId);
        }

        public Comment GetComment(int commentId)
        {
            return dbContext.Comments.Find(commentId);
        }
    }
}
