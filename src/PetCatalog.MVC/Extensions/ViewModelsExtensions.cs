using PetCatalog.Application.Interfaces;
using PetCatalog.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.MVC.Extensions
{
    public static class ViewModelsExtensions
    {
        public static void SetCategory(this AnimalViewModel animalVm,ICategoryService categoryService)
        {
            if (animalVm.CategoryId < 0)
            {
                categoryService.AddCategory(animalVm.CategoryName, out int cateId);
                animalVm.CategoryId = cateId;
            }
        }       
    }

}
