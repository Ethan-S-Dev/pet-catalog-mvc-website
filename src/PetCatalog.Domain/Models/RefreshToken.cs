using System;
using System.ComponentModel.DataAnnotations;

namespace PetCatalog.Domain.Models
{
    public class RefreshToken
    {
        [Key]
        public int TokenId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool KeepLoggedIn { get; set; }
    }
}
