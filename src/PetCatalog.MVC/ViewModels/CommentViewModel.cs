using System.ComponentModel.DataAnnotations;

namespace PetCatalog.MVC.ViewModels
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        
        public int AnimalId { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [MaxLength(200)]
        public string Value { get; set; }
    }
}
