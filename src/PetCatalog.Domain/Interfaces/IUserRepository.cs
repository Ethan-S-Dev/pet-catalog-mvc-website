using PetCatalog.Domain.Models;

namespace PetCatalog.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User Get(User user);
        User Get(string email);
    }
}
