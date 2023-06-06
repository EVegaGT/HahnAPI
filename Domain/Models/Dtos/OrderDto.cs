using Infrastructure.Models.Enums;


namespace Domain.Models.Dtos
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public string? NumberOrder { get; set; }
        public decimal Total { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Guid UserId { get; set; }

        public UserDto? User { get; set; }
        public List<OrdersProductsDto>? OrdersProducts { get; set; }
    }
}
