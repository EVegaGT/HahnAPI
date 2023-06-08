using Domain.Models.Responses.Product;
using MediatR;

namespace Domain.Services.Product.Command
{
    public class UpdateProductCommand : IRequest<ProductResponse>
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid BrandId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
