namespace Domain.Models.Dtos
{
    public class BrandDto
    {
        public Guid BrandId { get; set; }
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }

        public List<ProductDto>? Products { get; set; }
    }
}
