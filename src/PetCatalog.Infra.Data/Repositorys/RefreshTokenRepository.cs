using PetCatalog.Domain.Interfaces;
using PetCatalog.Domain.Models;
using PetCatalog.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Infra.Data.Repositorys
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly PetCatalogDbContext dbContext;
        public RefreshTokenRepository(PetCatalogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Create(RefreshToken obj)
        {
            dbContext.RefreshTokens.Add(obj);
            dbContext.SaveChanges();
        }

        public RefreshToken Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public RefreshToken Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RefreshToken> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RefreshToken> GetRecentTokens(string token)
        {
            return dbContext.RefreshTokens.Where(rt => rt.Token == token).OrderByDescending(rt => rt.ExpiryDate);
        }

        public void Update(RefreshToken obj)
        {
            throw new NotImplementedException();
        }
    }
}
