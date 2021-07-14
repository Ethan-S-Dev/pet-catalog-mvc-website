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
        Animal GetAnimal(int animalId);       

        IEnumerable<Animal> GetBestAnimals();
        IEnumerable<Animal> GetAllAnimals();

        bool AddAnimal(Animal animal);

        void EditAnimal(Animal animal);

        void DeleteAnimal(int animalId);

        public Animal GetEmptyAnimal();

        void DeleteComment(int id);

        void AddComment(Comment comment);
    }
}
