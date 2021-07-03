using PetShop.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.Interfaces
{
    public interface ICategoryService
    {
        public IEnumerable<CategoryViewModel> GetCategorys();

        bool AddCategory(string name,out int id);
        bool AddCategory(string name);
    }
}
