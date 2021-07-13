using PetCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Application.Auth
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
