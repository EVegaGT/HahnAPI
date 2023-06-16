using Domain.Models.Requests.Order;
using MediatR;

namespace Domain.Services.Order.Commands
{
    public class PatchOrderProductsCommand : IRequest<bool>
    {
        public Guid OrderId { get; set; }
        public List<PatchProductOrderRequest> Products { get; set; } = new();
    }
}
