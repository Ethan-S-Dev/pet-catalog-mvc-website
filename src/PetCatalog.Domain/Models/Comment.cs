namespace PetCatalog.Domain.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int AnimalId { get; set; }
        public virtual Animal Animal { get; set; }
        public string Value { get; set; }
    }
}
