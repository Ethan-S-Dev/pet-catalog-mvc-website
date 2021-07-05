using PetCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Application.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        [Display(Name="Category: ")]
        public string Name { get; set; }
        public IEnumerable<AnimalViewModel> Animals { get; set; }
    }
}
