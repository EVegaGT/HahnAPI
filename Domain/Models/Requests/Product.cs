namespace Domain.Models.Requests
{
    public class ProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid BrandId { get; set; }
        public Guid CategoryId { get; set; }
    }
}