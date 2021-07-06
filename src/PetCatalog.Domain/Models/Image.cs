using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Domain.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }

    }
}
