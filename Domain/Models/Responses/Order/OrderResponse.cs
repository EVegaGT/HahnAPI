using Infrastructure.Models.Enums;

namespace Domain.Models.Responses.Order
{
    public class OrderResponse
    {

        public Guid OrderId { get; set; }
        public string? NumberOrder { get; set; }
        public decimal Total { get; set; }
        public OrderStatus Status { get; set; }
        public Guid UserId { get; set; }

        public List<OrdersProductsResponse>? OrdersProducts { get; set; }
    }
}
