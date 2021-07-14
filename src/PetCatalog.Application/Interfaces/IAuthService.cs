using Microsoft.IdentityModel.Tokens;
using PetCatalog.Domain.Auth;
using PetCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Application.Interfaces
{
    public interface IAuthService
    {
        UserWithToken Authenticate(User user,bool keepLoggedIn = false);

        UserWithToken RefreshToken(RefreshRequest refreshRequest);

        void DeleteRefreshToken(RefreshRequest refreshRequest);
        void DeleteAllRefreshToken(RefreshRequest refreshRequest);

        User GetEmptyUser();

    }
}
