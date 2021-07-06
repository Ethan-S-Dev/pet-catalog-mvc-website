using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.MVC.ViewModels
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        [Required]
        
        public int AnimalId { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [MaxLength(200)]
        public string Value { get; set; }
    }
}
