using MediatR;

namespace Domain.Services.Order.Commands
{
    public class CreateUserOrderCommand : IRequest<Guid>
    {
    }
}
