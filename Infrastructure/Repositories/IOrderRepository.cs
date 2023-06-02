using Infrastructure.Models;

namespace Infrastructure.Repositories
{
    public interface IOrderRepository
    {
        Task CreateOrder(Order order);
        Task UpdateOrder(Order order);
    }
}