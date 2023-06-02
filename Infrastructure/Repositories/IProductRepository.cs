using Infrastructure.Models;

namespace Infrastructure.Repositories
{
    public interface IProductRepository
    {
        Task CreateProduct(Product product);
        Task UpdateProduct(Product product);
    }
}