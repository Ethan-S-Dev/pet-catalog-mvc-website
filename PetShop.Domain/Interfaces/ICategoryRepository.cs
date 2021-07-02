using PetShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        // Create
        void AddCategory(Category category);

        // Read
        IEnumerable<Category> GetCategorys();

        // Update

        // Delete
    }
}
