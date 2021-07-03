using PetShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.ViewModels
{
    public class AnimalViewModel
    {
        public int AnimalId { get; set; }

        public int CategoryId { get; set; }

        public string PictureName { get; set; }    

        [Required(ErrorMessage = "Please enter a name.")]
        [Display(Name = "Animal Name: ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter an age.")]
        [Range(0, 200)]
        [Display(Name = "Animal Age: ")]
        public int Age { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a description for the animal.")]
        [MaxLength(500, ErrorMessage = "Description can't be longer then 500 characters.")]
        public string Description { get; set; }

        public CategoryViewModel Category { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
