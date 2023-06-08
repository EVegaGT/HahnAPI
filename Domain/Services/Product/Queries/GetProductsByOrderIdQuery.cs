using Domain.Models.Responses.Product;
using MediatR;

namespace Domain.Services.Product.Queries
{
    public class GetProductsByOrderIdQuery : IRequest<List<GetProductResponse>>
    {
        public Guid OrderId { get; set; }
        public GetProductsByOrderIdQuery(Guid orderId) => OrderId = orderId;
    }
}
