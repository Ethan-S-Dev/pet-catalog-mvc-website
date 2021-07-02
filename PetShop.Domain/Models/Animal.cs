using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.Models
{
    public class Animal
    {
        public int AnimalId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string PictureName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}
