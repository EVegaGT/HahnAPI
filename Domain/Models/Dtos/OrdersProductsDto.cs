namespace Domain.Models.Dtos
{
    public class OrdersProductsDto
    {
        public Guid OrderProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }

        public ProductDto? Product { get; set; }
        public OrderDto? Order { get; set; }
    }
}
