using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
