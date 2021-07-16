using PetCatalog.Domain.Models;
using System.Linq;

namespace PetCatalog.Domain.Auth
{
    public class UserWithToken : User
    {
        public UserWithToken(User user)
        {
            UserId = user.UserId;
            Email = user.Email;
            Name = user.Name;
            RefreshToken = user.RefreshTokens.FirstOrDefault();
        }
        public string AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
