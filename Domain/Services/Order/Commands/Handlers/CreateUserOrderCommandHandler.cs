using MediatR;

namespace Domain.Services.Order.Commands.Handlers
{
    public class CreateUserOrderCommandHandler : IRequestHandler<CreateUserOrderCommand, Guid>
    {
        public Task<Guid> Handle(CreateUserOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
