using System.Collections.Generic;

namespace PetCatalog.Domain.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Animal> Animals { get; set; }
    }
}
