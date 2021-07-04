using Microsoft.AspNetCore.Http;
using PetShop.Application.Interfaces;
using PetShop.Application.ViewModels;
using PetShop.MVC.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.MVC.Models
{
    public class AnimalEditModel
    {
        public AnimalEditModel()
        {

        }
        public AnimalEditModel(ICategoryService categoryService)
        {
            Categorys = categoryService.GetCategorys();
        }


        public AnimalViewModel Animal { get; set; }

        
        [DataType(DataType.Upload)]
        [AllowedExtensions(".png", ".jpg", ".svg", ".jpeg", ".webp", ErrorMessage = "Invalid image type.")]
        [MaxFileSize(65536, ErrorMessage = "Image must be smaller then 64 KB.")]
        [Display(Name = "Animal Image: ")]
        public IFormFile Image { get; set; }

        public IEnumerable<CategoryViewModel> Categorys { get; set; }

        [CategoryId(ErrorMessage = "You must select a category or create a new one.")]
        public int CategoryId { get; set; }

        [CategoryName]
        [Required(ErrorMessage = "Category name can't be empty.")]
        public string CategoryName { get; set; }
    }
}
