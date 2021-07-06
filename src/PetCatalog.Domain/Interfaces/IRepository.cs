using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Domain.Interfaces
{
    public interface IRepository<T>
    {
        //Create
        void Create(T obj);

        // Read
        public IEnumerable<T> GetAll();
        public T Get(int id);

        // Update
        void Update(T animal);
        // Delete
        T Delete(int animalId);
    }
}
