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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {           
            this.categoryRepository = categoryRepository;
        } 

        public bool AddCategory(string name)
        {
            if (!categoryRepository.GetAll().All(c => c.Name.ToLower() != name.ToLower())) return false;
            categoryRepository.Create(new Category() { Name = name });
            return true;
        }

        public bool AddCategory(string name, out int id)
        {
            id = default;
            if (!categoryRepository.GetAll().All(c => c.Name.ToLower() != name.ToLower())) return false;
            var cate = new Category() { Name = name };
            categoryRepository.Create(cate);
            id = cate.CategoryId;
            return true;
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
            return categoryRepository.GetAll().ToList();         
        }
    }
}
