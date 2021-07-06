using PetCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Domain.Interfaces
{
    public interface IAnimalRepository : IRepository<Animal>
    {
        // Read       
        public IEnumerable<Animal> GetTopCommented();
    }
}
