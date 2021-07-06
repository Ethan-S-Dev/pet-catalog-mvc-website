using PetCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        // Create
        void AddCategory(Category category);

        // Read
        IEnumerable<Category> GetCategorys();
        Category GetCategory(int categoryId);

        // Update

        // Delete
    }
}
