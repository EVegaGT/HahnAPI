using Infrastructure.Models;

namespace Infrastructure.Repositories.ReadOnlyRepositories
{
    public interface IRoleReadOnlyRepository
    {
        Task<Role?> GetRoleById(Guid roleId);
        Task<List<Role>> GetRoles(Guid roleId);
    }
}