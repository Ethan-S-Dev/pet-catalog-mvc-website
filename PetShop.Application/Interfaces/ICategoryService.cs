using PetCatalog.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Application.Interfaces
{
    public interface ICategoryService
    {
        public IEnumerable<CategoryViewModel> GetCategorys();

        public CategoryViewModel GetCategory(int categoryId);
        bool AddCategory(string name,out int id);
        bool AddCategory(string name);
    }
}
