using PetCatalog.Domain.Auth;
using PetCatalog.Domain.Models;

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
