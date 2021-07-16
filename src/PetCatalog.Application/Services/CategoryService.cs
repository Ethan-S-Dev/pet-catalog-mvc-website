using PetCatalog.Application.Interfaces;
using PetCatalog.Domain.Interfaces;
using PetCatalog.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace PetCatalog.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {           
            this.categoryRepository = categoryRepository;
        } 
        public IEnumerable<Animal> GetAnimals(int categoryId)
        {
            return categoryRepository.Get(categoryId).Animals.OrderBy(ani => ani.Name.ToLower()).ToList();
        }
        public Category GetCategory(int categoryId)
        {
            return categoryRepository.Get(categoryId);           
        }
        public IEnumerable<Category> GetCategorys()
        {
            var ret = categoryRepository.GetAll().ToList();
            return ret;
        }
    }
}
