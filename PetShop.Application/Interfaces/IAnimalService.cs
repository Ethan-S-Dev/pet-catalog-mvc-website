using PetShop.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.Interfaces
{
    public interface IAnimalService
    {
        public AnimalViewModel GetAnimal(int animalId);

        public BestAnimalsViewModel GetBestAnimals();
    }
}
