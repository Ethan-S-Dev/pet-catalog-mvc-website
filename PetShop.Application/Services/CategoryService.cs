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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        public CategoryService(ICategoryRepository categoryRepository,IMapper mapper)
        {
            this.mapper = mapper;
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

        public CategoryViewModel GetCategory(int categoryId)
        {
            var category = categoryRepository.GetCategory(categoryId);
            return mapper.Map<CategoryViewModel>(category);
           
        }

        public IEnumerable<CategoryViewModel> GetCategorys()
        {
            var cateList = categoryRepository.GetCategorys();
            var retCateList = new List<CategoryViewModel>(cateList.Count());
            foreach (var cate in cateList)
                retCateList.Add(mapper.Map<CategoryViewModel>(cate));

            return retCateList;
           
        }
    }
}
