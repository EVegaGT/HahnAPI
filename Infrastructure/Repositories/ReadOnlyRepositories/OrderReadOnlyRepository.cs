using Infrastructure.Context;
using Infrastructure.Models;
using Infrastructure.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.ReadOnlyRepositories
{
    public class OrderReadOnlyRepository : IOrderReadOnlyRepository
    {
        private readonly HahnAPIContext _dbContext;

        public OrderReadOnlyRepository(HahnAPIContext contextFactory)
        {
            _dbContext = contextFactory;
        }

        public async Task<Order?> GetOrderById(Guid orderId)
        {
            return await _dbContext.Orders.Where(x => x.OrderId == orderId)
                    .Include(x => x.User)
                    .Include(x => x.OrdersProducts)
                    .FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetOrdersByUser(Guid userId)
        {
            return await _dbContext.Orders.Where(x => x.UserId == userId)
                    .Include(x => x.User)
                    .Include(x => x.OrdersProducts)
                    .ToListAsync();
        }

        public async Task<List<Order>> GetOrdersByProductId(Guid productId)
        {
            return await _dbContext.Orders
                    .Include(x => x.User)
                    .Include(x => x.OrdersProducts)
                    .Where(x => x.OrdersProducts != null && x.OrdersProducts.Any(o => o.ProductId == productId))
                    .ToListAsync();
        }

        public async Task<List<Order>> GetActiveOrdes()
        {
            return await _dbContext.Orders
                    .Where(x => x.Status == OrderStatus.InProgress)
                    .Include(x => x.User)
                    .Include(x => x.OrdersProducts)
                    .ToListAsync();
        }

        public async Task<List<Order>> GetAllOrdes()
        {
            return await _dbContext.Orders
                    .Include(x => x.User)
                    .Include(x => x.OrdersProducts)
                    .ToListAsync();
        }
    }
}
