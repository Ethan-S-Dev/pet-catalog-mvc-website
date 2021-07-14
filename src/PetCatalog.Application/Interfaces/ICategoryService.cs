using PetCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Application.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategorys();
        Category GetCategory(int categoryId);
        IEnumerable<Animal> GetAnimals(int categoryId);

    }
}
