using PetShop.Application.Interfaces;
using PetShop.Application.ViewModels;
using PetShop.Domain.Interfaces;
using PetShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository animalRepository;
        public AnimalService(IAnimalRepository animalRepository)
        {
            this.animalRepository = animalRepository;
        }        

        public bool AddAnimal(AnimalViewModel animal, out int id)
        {
            var ani = new Animal()
            {
                Name = animal.Name,
                Age = animal.Age,
                CategoryId = animal.CategoryId,
                Description = animal.Description,
                PictureName = animal.ImageName
            };
            animalRepository.AddAnimal(ani);
            id = ani.AnimalId;
            return true;
        }

        public bool AddAnimal(AnimalViewModel animal)
        {
            var ani = new Animal()
            {
                Name = animal.Name,
                Age = animal.Age,
                CategoryId = animal.CategoryId,
                Description = animal.Description,
                PictureName = animal.ImageName
            };
            animalRepository.AddAnimal(ani);

            return true;
        }

        public AnimalViewModel GetAnimal(int animalId)
        {
            var animal = animalRepository.GetAnimal(animalId);
            return new AnimalViewModel()
            {
                AnimalId = animal.AnimalId,
                Name = animal.Name,
                Age = animal.Age,
                CategoryId = animal.CategoryId,
                Description = animal.Description,
                ImageName = animal.PictureName
            };
        }
        public BestAnimalsViewModel GetBestAnimals()
        {
            var bestAnimals = new List<AnimalViewModel>(2);
            foreach (var animal in animalRepository.GetBestAnimals())
                bestAnimals.Add(new AnimalViewModel()
                {
                    AnimalId = animal.AnimalId,
                    Name = animal.Name,
                    Age = animal.Age,
                    CategoryId = animal.CategoryId,
                    Description = animal.Description,
                    ImageName = animal.PictureName
                });
            
            return new BestAnimalsViewModel()
            {
                Animals = bestAnimals
            };
        }
    }
}
