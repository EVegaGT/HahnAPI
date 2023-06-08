using Domain.Models.Responses.Product;
using MediatR;

namespace Domain.Services.Product.Queries
{
    public class GetProductByIdQuery : IRequest<GetProductResponse>
    {
        public Guid ProductId { get; set; }
        public GetProductByIdQuery(Guid productId) => ProductId = productId;
    }
}
