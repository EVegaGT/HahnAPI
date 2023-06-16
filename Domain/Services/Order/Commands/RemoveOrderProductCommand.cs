using Domain.Models.Requests.Order;
using MediatR;

namespace Domain.Services.Order.Commands
{
    public class RemoveOrderProductCommand : IRequest<bool>
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
    }
}