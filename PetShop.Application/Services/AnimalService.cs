using PetShop.Application.Interfaces;
using PetShop.Application.ViewModels;
using PetShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.Services
{
    public class AnimalService : ICatalogService
    {
        private readonly ICategoryRepository categoryRepository;
        public AnimalService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public CatalogViewModel GetCatagorys()
        {
            return new CatalogViewModel()
            {
                Categorys = categoryRepository.GetCategorys()
            };
        }
    }
}
