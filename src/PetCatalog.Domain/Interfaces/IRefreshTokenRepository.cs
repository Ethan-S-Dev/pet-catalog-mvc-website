using PetCatalog.Domain.Models;
using System.Collections.Generic;

namespace PetCatalog.Domain.Interfaces
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        IEnumerable<RefreshToken> GetRecentTokens(string token);
        void DeleteUserToken(User user, string token);
        void DeleteAllUserTokens(User user);
    }
}
