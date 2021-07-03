using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.ViewModels
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public int AnimalId { get; set; }
        public virtual AnimalViewModel Animal { get; set; }
        public string Value { get; set; }
    }
}
