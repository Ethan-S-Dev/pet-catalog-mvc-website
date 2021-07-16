using PetCatalog.Application.Interfaces;
using PetCatalog.Domain.Interfaces;
using PetCatalog.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace PetCatalog.Application.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository animalRepository;
        private readonly IImageRepository imageRepository;
        private readonly ICommentRepository commentRepository;
        private readonly ICategoryRepository categoryRepository;

        public AnimalService(IAnimalRepository animalRepository,IImageRepository imageRepository,ICommentRepository commentRepository,ICategoryRepository categoryRepository)
        {
            this.animalRepository = animalRepository;
            this.imageRepository = imageRepository;
            this.commentRepository = commentRepository;
            this.categoryRepository = categoryRepository;
        }        
        public bool AddAnimal(Animal animal)
        {
            if (animal.CategoryId < 0)
            {
                var cate = categoryRepository.GetAll().FirstOrDefault(c => c.Name.ToLower() == animal.Category.Name.ToLower());
                if (cate is null)
                {
                    cate = new Category() { Name = animal.Category.Name };
                    categoryRepository.Create(cate);
                    animal.CategoryId = cate.CategoryId;
                    animal.Category = cate;
                }
                else
                {
                    animal.CategoryId = cate.CategoryId;
                    animal.Category = cate;
                }
            }

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
        public void AddComment(Comment comment)
        {
            commentRepository.Create(comment);
        }
        public void DeleteAnimal(int animalId)
        {
            var animal = animalRepository.Delete(animalId);
            commentRepository.DeleteAnimalComments(animalId);
            imageRepository.Delete(animal.ImageId);
        }
        public void DeleteComment(int id)
        {
            commentRepository.Delete(id);
        }
        public void EditAnimal(Animal animal)
        {
            if (animal.CategoryId < 0)
            {
                var cate = categoryRepository.GetAll().FirstOrDefault(c => c.Name.ToLower() == animal.Category.Name.ToLower());
                if (cate is null)
                {
                    cate = new Category() { Name = animal.Category.Name };
                    categoryRepository.Create(cate);
                    animal.CategoryId = cate.CategoryId;
                    animal.Category = cate;
                }
                else
                {
                    animal.CategoryId = cate.CategoryId;
                    animal.Category = cate;
                }
            }

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
        public IEnumerable<Animal> GetAllAnimals()
        {
            return animalRepository.GetAll().ToList();
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
