using Infrastructure.Context;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly HahnAPIContext _dbContext;

        public RoleRepository(HahnAPIContext contextFactory)
        {
            _dbContext = contextFactory;
        }

        public async Task CreateRole(Role role)
        {
            await _dbContext.Roles.AddAsync(role);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRole(Role role)
        {
            _dbContext.Entry(role).State = EntityState.Modified;
            _dbContext.Roles.Update(role);
            await _dbContext.SaveChangesAsync();
        }
    }
}
