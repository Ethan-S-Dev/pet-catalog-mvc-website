using PetCatalog.Domain.Models;
using System.Collections.Generic;

namespace PetCatalog.Domain.Interfaces
{
    public interface IAnimalRepository : IRepository<Animal>
    {
        // Read       
        IEnumerable<Animal> GetTopCommented();
    }
}
