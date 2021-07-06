using Microsoft.AspNetCore.Http;
using PetCatalog.Application.Interfaces;
using PetCatalog.MVC.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.MVC.ViewModels
{
    public class AnimalEditModel
    {
        [Required]
        public AnimalViewModel Animal { get; set; }

        
        [DataType(DataType.Upload)]
        [AllowedExtensions(".png", ".jpg", ".svg", ".jpeg", ".webp", ErrorMessage = "Invalid image type.")]
        [MaxFileSize(204800, ErrorMessage = "Image must be smaller then 200 KB.")]
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
