using Infrastructure.Context;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.ReadOnlyRepositories
{
    public class BrandReadOnlyRepository : IBrandReadOnlyRepository
    {
        private readonly HahnAPIContext _dbContext;

        public BrandReadOnlyRepository(HahnAPIContext contextFactory)
        {
            _dbContext = contextFactory;
        }

        public async Task<Brand?> GetBrandById(Guid brandId)
        {
            return await _dbContext.Brands.FirstOrDefaultAsync(x => x.BrandId == brandId);
        }

        public async Task<List<Brand>> GetBrands()
        {
            return await _dbContext.Brands.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<bool> ExistBrandById(Guid brandId)
        {
            return await _dbContext.Brands.AnyAsync(x => x.BrandId == brandId && !x.IsDeleted);
        }
    }
}
