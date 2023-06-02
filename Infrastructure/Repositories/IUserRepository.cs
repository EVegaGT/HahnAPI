using Infrastructure.Models;

namespace Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task CreateUser(User user);
        Task UpdateUser(User user);
    }
}