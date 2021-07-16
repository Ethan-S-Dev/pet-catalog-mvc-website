namespace PetCatalog.Domain.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}
