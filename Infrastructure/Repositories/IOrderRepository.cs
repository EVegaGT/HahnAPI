using Infrastructure.Models;

namespace Infrastructure.Repositories
{
    public interface IOrderRepository
    {
        Task CreateOrder(Order order);
        Task UpdateOrder(Order order);
        Task AddOrderProduct(OrdersProducts orderProduct);
        Task UpdateOrderProduct(OrdersProducts orderProduct);
        Task RemoveOrderProduct(Guid orderId, Guid productId);
    }
}