using AutoMapper;
using PetCatalog.Application.ViewModels;
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
        public IEnumerable<Category> GetCategorys();
        public Category GetCategory(int categoryId);
        bool AddCategory(string name,out int id);
        bool AddCategory(string name);
    }
}
