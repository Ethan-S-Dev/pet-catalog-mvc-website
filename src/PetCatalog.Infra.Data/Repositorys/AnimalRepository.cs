using PetCatalog.Domain.Interfaces;
using PetCatalog.Domain.Models;
using PetCatalog.Infra.Data.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace PetCatalog.Infra.Data.Repositorys
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly PetCatalogDbContext dbContext;
        public AnimalRepository(PetCatalogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Create(Animal animal)
        {
            dbContext.Animals.Add(animal);
            dbContext.SaveChanges();
        }
        public Animal Delete(int animalId)
        {
            var animal = Get(animalId);
            if (animal is null) return null;
            dbContext.Animals.Remove(animal);
            dbContext.SaveChanges();
            return animal;
        }
        public void Update(Animal animal)
        {
            var realAnimal = dbContext.Animals.Find(animal.AnimalId);
            if (realAnimal is null) return;

            realAnimal.Age = animal.Age;
            realAnimal.CategoryId = animal.CategoryId;
            realAnimal.Name = animal.Name;
            realAnimal.Description = animal.Description;
            realAnimal.ImageId = animal.ImageId;

            dbContext.SaveChanges();
        }
        public Animal Get(int animalId)
        {
            return dbContext.Animals.Find(animalId);
        }
        public IEnumerable<Animal> GetAll()
        {
            return dbContext.Animals.OrderBy(ani=>ani.Name.ToLower());
        }
        public IEnumerable<Animal> GetTopCommented()
        {
            return dbContext.Animals.OrderByDescending(a => a.Comments.Count()).Take(2);
        }
    }
}
