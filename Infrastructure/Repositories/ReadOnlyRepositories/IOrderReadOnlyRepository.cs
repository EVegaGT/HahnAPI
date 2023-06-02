using Infrastructure.Models;

namespace Infrastructure.Repositories.ReadOnlyRepositories
{
    public interface IOrderReadOnlyRepository
    {
        Task<List<Order>> GetActiveOrdes();
        Task<List<Order>> GetAllOrdes();
        Task<Order?> GetOrderById(Guid orderId);
        Task<List<Order>> GetOrdersByProductId(Guid productId);
        Task<List<Order>> GetOrdersByUser(Guid userId);
    }
}