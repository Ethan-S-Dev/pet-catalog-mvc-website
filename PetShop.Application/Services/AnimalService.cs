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
        public AnimalViewModel GetAnimal(int animalId)
        {
            return new AnimalViewModel()
            {
                Animal = animalRepository.GetAnimal(animalId)
            };
        }
        public BestAnimalsViewModel GetBestAnimals()
        {
            var bestAnimals = new List<AnimalViewModel>(2);
            foreach (var animal in animalRepository.GetBestAnimals())
                bestAnimals.Add(new AnimalViewModel()
                {
                    Animal = animal
                });
            
            return new BestAnimalsViewModel()
            {
                Animals = bestAnimals
            };
        }
    }
}
