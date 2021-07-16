using PetCatalog.Domain.Models;
using System.Collections.Generic;

namespace PetCatalog.Application.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategorys();
        Category GetCategory(int categoryId);
        IEnumerable<Animal> GetAnimals(int categoryId);
    }
}
