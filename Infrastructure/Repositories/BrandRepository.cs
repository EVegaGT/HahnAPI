using Infrastructure.Context;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly HahnAPIContext _dbContext;

        public BrandRepository(HahnAPIContext contextFactory)
        {
            _dbContext = contextFactory;
        }

        public async Task CreateBrand(Brand brand)
        {
            await _dbContext.Brands.AddAsync(brand);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateBrand(Brand brand)
        {
            _dbContext.Entry(brand).State = EntityState.Modified;
            _dbContext.Brands.Update(brand);
            await _dbContext.SaveChangesAsync();
        }
    }
}
