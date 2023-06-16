using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Models.Dtos;
using Domain.Models.Responses.Order;
using Infrastructure.Repositories.ReadOnlyRepositories;
using MediatR;

namespace Domain.Services.Order.Queries.Handlers
{
    public class GetActiveOrdersQueryHandler : IRequestHandler<GetActiveOrdersQuery, List<OrderResponse>>
    {
        private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
        private readonly IMapper _mapper;

        public GetActiveOrdersQueryHandler(IOrderReadOnlyRepository orderReadOnlyRepository, IMapper mapper)
        {
            _orderReadOnlyRepository = orderReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderResponse>> Handle(GetActiveOrdersQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new HahnApiException(ErrorCodeEnum.OrderRequestIsRequired);

            try
            {
                var orders = _mapper.Map<List<OrderDto>>(await _orderReadOnlyRepository.GetActiveOrdes());
                return orders != null ? _mapper.Map<List<OrderResponse>>(orders) : throw new HahnApiException(ErrorCodeEnum.OrderNotFound);
            }
            catch (HahnApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HahnApiException(ErrorCodeEnum.InvalidRequest, $"Could not get active orders. {ex.Message}");
            }
        }
    }
}