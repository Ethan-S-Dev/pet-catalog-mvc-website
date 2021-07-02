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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public bool AddCategory(string name)
        {
            if (!categoryRepository.GetCategorys().All(c => c.Name.ToLower() != name.ToLower())) return false;
            categoryRepository.AddCategory(new Category() { Name = name });
            return true;
        }

        public bool AddCategory(string name, out int id)
        {
            id = default;
            if (!categoryRepository.GetCategorys().All(c => c.Name.ToLower() != name.ToLower())) return false;
            var cate = new Category() { Name = name };
            categoryRepository.AddCategory(cate);
            id = cate.CategoryId;
            return true;
        }

        public CategoryViewModel GetCategorys()
        {
            return new CategoryViewModel()
            {
                Categorys = categoryRepository.GetCategorys()
            };
        }
    }
}
