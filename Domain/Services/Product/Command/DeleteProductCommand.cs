using MediatR;

namespace Domain.Services.Product.Command
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public Guid ProductId { get; set; }
        public DeleteProductCommand(Guid productId) => ProductId = productId;
    }
}
