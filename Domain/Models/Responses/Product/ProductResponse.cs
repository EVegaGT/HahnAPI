using Infrastructure.Models.Enums;

namespace Domain.Models.Responses.Product
{
    public class ProductResponse
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductStatus Status { get; set; }
        public Guid BrandId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
