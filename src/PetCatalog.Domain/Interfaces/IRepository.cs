using System.Collections.Generic;

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
        void Update(T obj);
        // Delete
        T Delete(int Id);
    }
}
