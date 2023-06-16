namespace Domain.Models.Responses.Order
{
    public class OrdersProductsResponse
    {
        public Guid OrderProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public Guid ProductId { get; set; }
    }
}
