using Infrastructure.Models;

namespace Infrastructure.Repositories.ReadOnlyRepositories
{
    public interface IProductReadOnlyRepository
    {
        Task<Product?> GetProductById(Guid productId);
        Task<List<Product>> GetProducts();
        Task<List<Product?>> GetProductsOrder(Guid orderId);
        Task<bool> ExistProduct(Guid productId);
        Task<decimal> GetProductPriceById(Guid productId);
    }
}