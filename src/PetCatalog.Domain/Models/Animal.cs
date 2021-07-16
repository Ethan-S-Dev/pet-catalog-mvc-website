using System.Collections.Generic;

namespace PetCatalog.Domain.Models
{
    public class Animal
    {
        public int AnimalId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }    
        public string Description { get; set; }
        public int ImageId { get; set; }
        public virtual Image Image { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual IEnumerable<Comment> Comments { get; set; }    
    }
}
