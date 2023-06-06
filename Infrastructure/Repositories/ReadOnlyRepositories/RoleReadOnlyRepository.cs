using Infrastructure.Context;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.ReadOnlyRepositories
{
    public class RoleReadOnlyRepository : IRoleReadOnlyRepository
    {
        private readonly HahnAPIContext _dbContext;

        public RoleReadOnlyRepository(HahnAPIContext contextFactory)
        {
            _dbContext = contextFactory;
        }

        public async Task<Role?> GetRoleById(Guid roleId)
        {
            return await _dbContext.Roles.FirstOrDefaultAsync(x => x.RoleId == roleId);
        }

        public async Task<List<Role>> GetRoles(Guid roleId)
        {
            return await _dbContext.Roles.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<Role?> GetRoleByName(string name)
        {
            return await _dbContext.Roles.FirstOrDefaultAsync(x => !x.IsDeleted && !string.IsNullOrEmpty(x.Name) && string.Equals(x.Name.ToLower(), name.ToLower()));
        }
    }
}