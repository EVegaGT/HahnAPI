using Domain.Models.Responses.Order;
using MediatR;

namespace Domain.Services.Order.Queries
{
    public class GetActiveOrdersQuery : IRequest<List<OrderResponse>>
    {
    }
}
