using AutoMapper;
using PetCatalog.Application.Interfaces;
using PetCatalog.Application.ViewModels;
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
        private readonly IMapper mapper;
        public AnimalService(IAnimalRepository animalRepository,IMapper mapper)
        {
            this.mapper = mapper;
            this.animalRepository = animalRepository;
        }        

        public bool AddAnimal(AnimalViewModel animal, out int id)
        {
            var ani = mapper.Map<Animal>(animal);
            animalRepository.AddAnimal(ani);
            id = ani.AnimalId;
            return true;
        }

        public bool AddAnimal(AnimalViewModel animal)
        {
            var ani = mapper.Map<Animal>(animal);
            animalRepository.AddAnimal(ani);
            return true;
        }

        public void DeleteAnimal(int animalId)
        {
            animalRepository.DeleteAnimal(animalId);           
        }

        public void EditAnimal(AnimalViewModel animal)
        {
            var ani = mapper.Map<Animal>(animal);
            animalRepository.EditAnimal(ani);
        }

        public AnimalViewModel GetAnimal(int animalId)
        {
            var animal = animalRepository.GetAnimal(animalId);
            return mapper.Map<AnimalViewModel>(animal);
        }
        public IEnumerable<AnimalViewModel> GetBestAnimals()
        {
            var bestAnimals = new List<AnimalViewModel>(2);
            foreach (var animal in animalRepository.GetBestAnimals())
                bestAnimals.Add(mapper.Map<AnimalViewModel>(animal));

            return bestAnimals;
        }
    }
}
