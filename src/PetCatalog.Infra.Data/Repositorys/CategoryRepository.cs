using PetCatalog.Domain.Interfaces;
using PetCatalog.Domain.Models;
using PetCatalog.Infra.Data.Contexts;
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

        public void Create(Category category)
        {
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
        }

        public Category Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Category Get(int categoryId)
        {
            return dbContext.Categories.Find(categoryId);
        }

        public IEnumerable<Category> GetAll()
        {
            return dbContext.Categories.OrderBy(cat=>cat.Name.ToLower());
        }

        public void Update(Category obj)
        {
            throw new NotImplementedException();
        }
    }
}
