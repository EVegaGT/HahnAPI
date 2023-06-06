using Infrastructure.Context;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.ReadOnlyRepositories
{
    public class UserReadOnlyRepository : IUserReadOnlyRepository
    {
        private readonly HahnAPIContext _dbContext;

        public UserReadOnlyRepository(HahnAPIContext contextFactory)
        {
            _dbContext = contextFactory;
        }

        public async Task<User?> GetUserById(Guid userId)
        {
            return await _dbContext.Users.Where(x => x.UserId == userId)
                    .Include(x => x.Role)
                    .FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbContext.Users.Where(x => x.Email == email && x.Active)
                .Include(x => x.Role)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ExistEmailUser(string email)
        {
            return await _dbContext.Users.AnyAsync(x => x.Email == email && x.Active);
        }
    }
}
