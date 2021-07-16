using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetCatalog.MVC.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        [Display(Name="Category: ")]
        public string Name { get; set; }

        public IEnumerable<AnimalViewModel> Animals { get; set; }
    }
}
