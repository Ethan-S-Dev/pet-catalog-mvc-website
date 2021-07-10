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
    public class UserRepository : IUserRepository
    {
        private readonly PetCatalogDbContext dbContext;
        public UserRepository(PetCatalogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Create(User obj)
        {
            throw new NotImplementedException();
        }

        public User Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public User Get(User user)
        {
            user = dbContext.Users.Where(u => (u.Email.ToLower() == user.Email.ToLower() && u.Password == user.Password)).FirstOrDefault();
            return user;
        }

        public User Get(string email)
        {
            return dbContext.Users.Where(usr => usr.Email == email).FirstOrDefault();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }
        public void Update(User obj)
        {
            throw new NotImplementedException();
        }
    }
}
