using PetShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        [Display(Name="Category: ")]
        public string Name { get; set; }
        public IEnumerable<AnimalViewModel> Animals { get; set; }
    }
}
