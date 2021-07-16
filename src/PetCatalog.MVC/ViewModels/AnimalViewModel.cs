using Microsoft.AspNetCore.Http;
using PetCatalog.MVC.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetCatalog.MVC.ViewModels
{
    public class AnimalViewModel
    {
        public int AnimalId { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        [Display(Name = "Animal Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter an age.")]
        [Range(0, 200)]
        [Display(Name = "Animal Age")]
        [DataType("Int32",ErrorMessage = "Age must be a natural number.")]
        public int Age { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please enter a description for the animal.")]
        [MaxLength(3000, ErrorMessage = "Description can't be longer then 3000 characters.")]
        public string Description { get; set; }

        public int ImageId { get; set; }

        [DataType(DataType.Upload)]
        [AllowedExtensions(".png", ".jpg", ".svg", ".jpeg", ".webp", ErrorMessage = "Invalid image type.")]
        [MaxFileSize(204800, ErrorMessage = "Image must be smaller then 200 KB.")]
        [Display(Name = "Animal Image")]
        public IFormFile Image {get;set;}

        [Display(Name="Category")]
        public int CategoryId { get; set; }

        [Display(Name ="Category name")]
        [Required(ErrorMessage = "Category name can't be empty.")]
        public string CategoryName { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
