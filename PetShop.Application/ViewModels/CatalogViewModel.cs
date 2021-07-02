using PetShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.ViewModels
{
    public class CatalogViewModel
    {
        public IEnumerable<Category> Categorys { get; set; }
    }
}
