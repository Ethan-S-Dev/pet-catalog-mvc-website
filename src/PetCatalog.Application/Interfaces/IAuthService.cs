using Microsoft.IdentityModel.Tokens;
using PetCatalog.Application.Auth;
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
        UserWithToken Authenticate(User user);

        UserWithToken RefreshToken(RefreshRequest refreshRequest);

        User GetEmptyUser();

    }
}
