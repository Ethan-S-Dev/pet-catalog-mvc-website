using PetCatalog.Domain.Interfaces;
using PetCatalog.Domain.Models;
using PetCatalog.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Infra.Data.Repositorys
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PetCatalogDbContext dbContext;
        public CategoryRepository(PetCatalogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddCategory(Category category)
        {
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
        }

        public Category GetCategory(int categoryId)
        {
            return dbContext.Categories.Find(categoryId);
        }

        public IEnumerable<Category> GetCategorys()
        {
            return dbContext.Categories;
        }
    }
}
