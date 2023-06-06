using Infrastructure.Models.Enums;

namespace Domain.Models.Dtos
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Guid BrandId { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryDto? Category { get; set; }
        public BrandDto? Brand { get; set; }
    }
}
