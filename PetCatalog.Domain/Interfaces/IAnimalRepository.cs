using PetCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Domain.Interfaces
{
    public interface IAnimalRepository
    {
        // Create
        public void AddAnimal(Animal animal);

        // Read
        public IEnumerable<Animal> GetAnimals();
        public Animal GetAnimal(int animalId);
        public IEnumerable<Animal> GetBestAnimals();

        // Update
        void EditAnimal(Animal animal);
        // Delete
        Animal DeleteAnimal(int animalId);
    }
}
