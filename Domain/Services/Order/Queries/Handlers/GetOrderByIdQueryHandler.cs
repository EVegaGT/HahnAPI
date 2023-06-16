using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Models.Dtos;
using Domain.Models.Responses.Order;
using Infrastructure.Repositories.ReadOnlyRepositories;
using MediatR;

namespace Domain.Services.Order.Queries.Handlers
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderResponse>
    {
        private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(IOrderReadOnlyRepository orderReadOnlyRepository, IMapper mapper)
        {
            _orderReadOnlyRepository = orderReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new HahnApiException(ErrorCodeEnum.OrderRequestIsRequired);

            try
            {
                var order = _mapper.Map<OrderDto>(await _orderReadOnlyRepository.GetOrderById(request.OrderId));
                return order != null ? _mapper.Map<OrderResponse>(order) : throw new HahnApiException(ErrorCodeEnum.OrderNotFound);
            }
            catch (HahnApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HahnApiException(ErrorCodeEnum.InvalidRequest, $"Could not get order by ID {request.OrderId}. {ex.Message}");
            }
        }
    }
}
