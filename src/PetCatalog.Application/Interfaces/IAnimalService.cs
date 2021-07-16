using PetCatalog.Domain.Models;
using System.Collections.Generic;

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
        Animal GetEmptyAnimal();
        void DeleteComment(int id);
        void AddComment(Comment comment);
    }
}
