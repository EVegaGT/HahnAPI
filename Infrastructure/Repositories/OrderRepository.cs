using Infrastructure.Context;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly HahnAPIContext _dbContext;

        public OrderRepository(HahnAPIContext contextFactory)
        {
            _dbContext = contextFactory;
        }

        public async Task CreateOrder(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddOrderProduct(OrdersProducts orderProduct)
        {
            await _dbContext.OrdersProducts.AddAsync(orderProduct);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateOrderProduct(OrdersProducts orderProduct)
        {
            _dbContext.Entry(orderProduct).State = EntityState.Modified;
            _dbContext.OrdersProducts.Update(orderProduct);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveOrderProduct(Guid orderId, Guid productId)
        {
            var cartItem = await _dbContext.OrdersProducts.FirstAsync(x => x.OrderId == orderId && x.ProductId == productId);

            _dbContext.OrdersProducts.Remove(cartItem);
            await _dbContext.SaveChangesAsync();
        }
    }
}
