using PetCatalog.Domain.Models;
using System.Collections.Generic;

namespace PetCatalog.Domain.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        IEnumerable<Comment> GetAnimalComments (int animalId);
        IEnumerable<Comment> DeleteAnimalComments(int animalId);
    }
}
