using Infrastructure.Models;

namespace Infrastructure.Repositories.ReadOnlyRepositories
{
    public interface IUserReadOnlyRepository
    {
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(Guid userId);
        Task<bool> ExistEmailUser(string email);
    }
}