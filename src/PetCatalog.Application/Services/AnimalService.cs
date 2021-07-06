using PetCatalog.Application.Interfaces;
using PetCatalog.Domain.Interfaces;
using PetCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Application.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository animalRepository;
        private readonly IImageRepository imageRepository;
        public AnimalService(IAnimalRepository animalRepository,IImageRepository imageRepository)
        {
            this.animalRepository = animalRepository;
            this.imageRepository = imageRepository;
        }        

        public bool AddAnimal(Animal animal, out int id)
        {
            animalRepository.Create(animal);
            imageRepository.Create(animal.Image);
            id = animal.AnimalId;
            return true;
        }

        public bool AddAnimal(Animal animal)
        {
            animalRepository.Create(animal);
            imageRepository.Create(animal.Image);
            return true;
        }

        public void DeleteAnimal(int animalId)
        {
            var animal = animalRepository.Delete(animalId);                    
            imageRepository.Delete(animal.ImageId);
        }

        public void EditAnimal(Animal animal)
        {
            var img = imageRepository.Get(animal.AnimalId);
            if (animal.Image.Name != img.Name)
                imageRepository.Update(animal.Image);
            animalRepository.Update(animal);
        }

        public Animal GetAnimal(int animalId)
        {
            return animalRepository.Get(animalId);
        }
        public IEnumerable<Animal> GetBestAnimals()
        {
            var bestAnimals = new List<Animal>(2);
            foreach (var animal in animalRepository.GetTopCommented())
                bestAnimals.Add(animal);
            return bestAnimals;
        }
    }
}
