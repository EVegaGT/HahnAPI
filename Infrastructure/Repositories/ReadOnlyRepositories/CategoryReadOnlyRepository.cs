using Infrastructure.Context;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.ReadOnlyRepositories
{
    public class CategoryReadOnlyRepository : ICategoryReadOnlyRepository
    {
        private readonly HahnAPIContext _dbContext;

        public CategoryReadOnlyRepository(HahnAPIContext contextFactory)
        {
            _dbContext = contextFactory;
        }

        public async Task<Category?> GetCategoryById(Guid categoryId)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == categoryId);
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _dbContext.Categories.Where(x => !x.IsDeleted).ToListAsync();
        }
    }
}
