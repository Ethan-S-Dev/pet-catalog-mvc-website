using PetShop.Domain.Interfaces;
using PetShop.Domain.Models;
using PetShop.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Infra.Data.Repositorys
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly PetShopDbContext dbContext;

        public AnimalRepository(PetShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddAnimal(Animal animal)
        {
            dbContext.Animals.Add(animal);
            dbContext.SaveChanges();
        }

        public Animal GetAnimal(int animalId)
        {
            return dbContext.Animals.Find(animalId);
        }

        public IEnumerable<Animal> GetAnimals()
        {
            return dbContext.Animals;
        }

        public IEnumerable<Animal> GetBestAnimals()
        {
            return dbContext.Animals.OrderByDescending(a => a.Comments.Count()).Take(2);
        }
    }
}
