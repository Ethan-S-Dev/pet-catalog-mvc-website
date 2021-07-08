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

        public bool AddAnimal(Animal animal)
        {
            var image = imageRepository.Get(animal.Image.ImageId);
            if (image is not null)
                animal.Image = image;
            else
            {
                imageRepository.Create(animal.Image);
                animal.ImageId = animal.Image.ImageId;
            }
            animalRepository.Create(animal);
            return true;
        }

        public void DeleteAnimal(int animalId)
        {
            var animal = animalRepository.Delete(animalId);                    
            imageRepository.Delete(animal.ImageId);
        }

        public void EditAnimal(Animal animal)
        {
            var realAnimal = animalRepository.Get(animal.AnimalId);

            if (animal.Image.ImageId == 0)
            {
                animal.Image.ImageId = realAnimal.ImageId;
                animal.ImageId = realAnimal.ImageId;
                imageRepository.Update(animal.Image);
                animal.ImageId = animal.Image.ImageId;
            }
            animalRepository.Update(animal);
        }

        public Animal GetAnimal(int animalId)
        {
            return animalRepository.Get(animalId);
        }
        public IEnumerable<Animal> GetBestAnimals()
        {
            var bestAnimals = animalRepository.GetTopCommented().ToList();
            return bestAnimals;
        }

        public Animal GetEmptyAnimal()
        {
            return new Animal { Image = imageRepository.Get(1) };
        }
    }
}
