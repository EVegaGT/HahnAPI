using Infrastructure.Models;

namespace Infrastructure.Repositories.ReadOnlyRepositories
{
    public interface IProductReadOnlyRepository
    {
        Task<Product?> GetProductById(Guid productId);
        Task<List<Product>> GetProducts();
    }
}