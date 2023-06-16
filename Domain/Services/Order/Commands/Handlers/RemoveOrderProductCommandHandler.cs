using AutoMapper;
using Infrastructure.Repositories.ReadOnlyRepositories;
using Infrastructure.Repositories;
using MediatR;
using Domain.Common.Exceptions;
using Domain.Models.Dtos;

namespace Domain.Services.Order.Commands.Handlers
{
    public class RemoveOrderProductCommandHandler : IRequestHandler<RemoveOrderProductCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
        private readonly IProductReadOnlyRepository _productReadOnlyRepositor;
        private readonly IMapper _mapper;

        public RemoveOrderProductCommandHandler(IOrderRepository orderRepository, IOrderReadOnlyRepository orderReadOnlyRepository, IProductReadOnlyRepository productReadOnlyRepositor, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _orderReadOnlyRepository = orderReadOnlyRepository;
            _productReadOnlyRepositor = productReadOnlyRepositor;
            _mapper = mapper;
        }

        public async Task<bool> Handle(RemoveOrderProductCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new HahnApiException(ErrorCodeEnum.OrderProductRequestIsRequired);

            try
            {
                if (!await _productReadOnlyRepositor.ExistProduct(request.ProductId)) throw new HahnApiException(ErrorCodeEnum.ProductNotFound);

                var order = await _orderReadOnlyRepository.GetOrderById(request.OrderId) ?? throw new HahnApiException(ErrorCodeEnum.OrderNotFound);
                var orderDto = _mapper.Map<OrderDto>(order);

                if (orderDto.OrdersProducts != null)
                {
                    var orderItem = orderDto.OrdersProducts.First(x => x.ProductId == request.ProductId);
                    orderDto.OrdersProducts.Remove(orderItem);
                    await _orderRepository.RemoveOrderProduct(orderDto.OrderId, request.ProductId);

                    //Update Order total
                    CalculateOrderTotal(orderDto);
                    orderDto.ModifiedAt = DateTime.UtcNow;
                    _mapper.Map(orderDto, order);
                    await _orderRepository.UpdateOrder(order);

                    return true;
                }

                return false;
            }
            catch (HahnApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HahnApiException(ErrorCodeEnum.InvalidRequest, $"Could not remove order product. {ex.Message}");
            }
        }

        /// <summary>
        /// Method to calculate the new total price for orderDto
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private void CalculateOrderTotal(OrderDto order)
        {
            //Update orderDto total
            if (order.OrdersProducts != null)
            {
                order.Total = order.OrdersProducts.Sum(orderItem => orderItem.Quantity * orderItem.Price);
            }
        }
    }
}
