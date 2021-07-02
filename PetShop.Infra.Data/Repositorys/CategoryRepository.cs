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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PetShopDbContext dbContext;
        public CategoryRepository(PetShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddCategory(Category category)
        {
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
        }

        public IEnumerable<Category> GetCategorys()
        {
            return dbContext.Categories;
        }
    }
}
