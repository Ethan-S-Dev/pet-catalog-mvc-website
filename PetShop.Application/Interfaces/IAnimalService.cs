using PetCatalog.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Application.Interfaces
{
    public interface IAnimalService
    {
        public AnimalViewModel GetAnimal(int animalId);       

        public IEnumerable<AnimalViewModel> GetBestAnimals();

        bool AddAnimal(AnimalViewModel animal);
        bool AddAnimal(AnimalViewModel animal,out int id);

        void EditAnimal(AnimalViewModel animal);

        void DeleteAnimal(int animalId);
    }
}
