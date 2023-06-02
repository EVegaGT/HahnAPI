using Infrastructure.Models;

namespace Infrastructure.Repositories
{
    public interface IRoleRepository
    {
        Task CreateRole(Role role);
        Task UpdateRole(Role role);
    }
}