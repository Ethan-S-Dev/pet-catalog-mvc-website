using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Domain.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Email { get; set; }
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }

        public virtual IEnumerable<RefreshToken> RefreshTokens { get; set; }
    }
}
