using Domain.Models.Responses.Order;
using MediatR;

namespace Domain.Services.Order.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderResponse>
    {
        public Guid OrderId { get; set; } 
        public GetOrderByIdQuery(Guid orderId) => OrderId = orderId;
    }
}
