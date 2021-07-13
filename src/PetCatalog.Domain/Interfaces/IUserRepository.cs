using PetCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCatalog.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User Get(User user);

        User Get(string email);
    }
}
