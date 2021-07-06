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
        public static void SetCategory(this AnimalViewModel animalVm, AnimalFormModel animalForm,ICategoryService categoryService)
        {
            if (animalForm.CategoryId < 0)
            {
                categoryService.AddCategory(animalForm.CategoryName, out int cateId);
                animalVm.CategoryId = cateId;
            }
            else
            {
                animalVm.CategoryId = animalForm.CategoryId;
            }
        }
        public static void SetCategory(this AnimalViewModel animalVm, AnimalEditModel animalForm, ICategoryService categoryService)
        {
            if (animalForm.CategoryId < 0)
            {
                categoryService.AddCategory(animalForm.CategoryName, out int cateId);
                animalVm.CategoryId = cateId;
            }
            else
            {
                animalVm.CategoryId = animalForm.CategoryId;
            }
        }
    }

}
