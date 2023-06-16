using Domain.Models.Responses.Order;
using MediatR;

namespace Domain.Services.Order.Queries
{
    public class GetOrdersQuery : IRequest<List<OrderResponse>>
    {
    }
}
