using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Animal> Animals { get; set; }
    }
}
