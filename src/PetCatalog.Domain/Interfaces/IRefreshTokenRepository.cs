using PetCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Domain.Interfaces
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        IEnumerable<RefreshToken> GetRecentTokens(string token);

        void DeleteUserToken(User user, string token);

        void DeleteAllUserTokens(User user);
    }
}
