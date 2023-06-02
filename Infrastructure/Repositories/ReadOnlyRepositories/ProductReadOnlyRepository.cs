﻿using Infrastructure.Context;
using Infrastructure.Models;
using Infrastructure.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.ReadOnlyRepositories
{
    public class ProductReadOnlyRepository : IProductReadOnlyRepository
    {
        private readonly HahnAPIContext _dbContext;

        public ProductReadOnlyRepository(HahnAPIContext contextFactory)
        {
            _dbContext = contextFactory;
        }

        public async Task<Product?> GetProductById(Guid productId)
        {
            return await _dbContext.Products.Where(x => x.ProductId == productId)
                    .Include(x => x.Brand)
                    .Include(x => x.Category)
                    .FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _dbContext.Products.Where(x => x.Status != ProductStatus.Inactive).ToListAsync();
        }
    }
}