using Infrastructure.Context;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly HahnAPIContext _dbContext;

        public ProductRepository(HahnAPIContext contextFactory)
        {
            _dbContext = contextFactory;
        }

        public async Task CreateProduct(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}
