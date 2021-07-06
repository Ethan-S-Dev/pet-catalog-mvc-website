using PetCatalog.Application.ViewModels;
using PetCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Application.Interfaces
{
    public interface IAnimalService
    {
        public Animal GetAnimal(int animalId);       

        public IEnumerable<Animal> GetBestAnimals();

        bool AddAnimal(Animal animal);
        bool AddAnimal(Animal animal,out int id);

        void EditAnimal(Animal animal);

        void DeleteAnimal(int animalId);
    }
}
