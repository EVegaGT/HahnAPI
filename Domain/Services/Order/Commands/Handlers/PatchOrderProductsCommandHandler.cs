using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Models.Dtos;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Repositories.ReadOnlyRepositories;
using MediatR;

namespace Domain.Services.Order.Commands.Handlers
{
    public class PatchOrderProductsCommandHandler : IRequestHandler<PatchOrderProductsCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
        private readonly IProductReadOnlyRepository _productReadOnlyRepositor;
        private readonly IMapper _mapper;

        public PatchOrderProductsCommandHandler(IOrderRepository orderRepository, IOrderReadOnlyRepository orderReadOnlyRepository, IProductReadOnlyRepository productReadOnlyRepositor, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _orderReadOnlyRepository = orderReadOnlyRepository;
            _productReadOnlyRepositor = productReadOnlyRepositor;
            _mapper = mapper;
        }

        public async Task<bool> Handle(PatchOrderProductsCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new HahnApiException(ErrorCodeEnum.OrderProductRequestIsRequired);

            try
            {
                var order = await _orderReadOnlyRepository.GetOrderById(request.OrderId) ?? throw new HahnApiException(ErrorCodeEnum.OrderNotFound);
                var orderDto = _mapper.Map<OrderDto>(order);
                var products = _mapper.Map<List<OrdersProductsDto>>(request.Products);

                if (products != null && products.Count > 0)
                {
                    await HandleOrderProducts(products, orderDto); // Add or update orderDto items
                    orderDto.ModifiedAt = DateTime.UtcNow;
                    CalculateOrderTotal(orderDto);
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
                throw new HahnApiException(ErrorCodeEnum.InvalidRequest, $"Could not add items to order. {ex.Message}");
            }
        }

        /// <summary>
        /// Verify if orderDto products should be updated or added
        /// </summary>
        /// <param name="productsItems"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        private async Task HandleOrderProducts(List<OrdersProductsDto> productsItems, OrderDto order)
        {
            foreach (var orderItem in productsItems)
            {
                orderItem.OrderId = order.OrderId;

                // verify if exist record and Update
                if (order.OrdersProducts != null && order.OrdersProducts.Count > 0 && order.OrdersProducts.Any(x => x.ProductId == orderItem.ProductId))
                {
                    await UpdateProductOrder(orderItem, order);
                }
                else // add new cart items
                {
                    await AddProductToOrder(orderItem, order);
                }
            }
        }

        /// <summary>
        /// Add new product to orderDto
        /// </summary>
        /// <param name="orderItem"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        private async Task AddProductToOrder(OrdersProductsDto orderItem, OrderDto order)
        {
            orderItem.OrderProductId = Guid.NewGuid();
            orderItem.CreatedAt = DateTime.UtcNow;
            orderItem.ModifiedAt = DateTime.UtcNow;
            orderItem.Quantity = orderItem.Quantity;
            orderItem.Price = await GetProductPrice(orderItem.ProductId);
            orderItem.Total = orderItem.Quantity * orderItem.Price;

            // added to orderDto products list to calculate new total
            if (order.OrdersProducts == null) order.OrdersProducts = new List<OrdersProductsDto>() { orderItem };
            else order.OrdersProducts.Add(orderItem);

            await Task.Run(() => _orderRepository.AddOrderProduct(_mapper.Map<OrdersProducts>(orderItem)));
        }

        /// <summary>
        /// Update orderDto product 
        /// </summary>
        /// <param name="orderItem"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        private async Task UpdateProductOrder(OrdersProductsDto orderItem, OrderDto order)
        {
            if (order.OrdersProducts != null)
            {
                var orderProduct = order.OrdersProducts.First(x => x.ProductId == orderItem.ProductId);
                orderProduct.Quantity = orderItem.Quantity;
                orderProduct.ModifiedAt = DateTime.UtcNow;
                orderProduct.Price = await GetProductPrice(orderProduct.ProductId);
                orderProduct.Total = orderProduct.Quantity * orderProduct.Price;
                await Task.Run(() => _orderRepository.UpdateOrderProduct(_mapper.Map<OrdersProducts>(orderProduct)));
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

        /// <summary>
        /// Get Produt Price
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        /// <exception cref="HahnApiException"></exception>
        private async Task<decimal> GetProductPrice(Guid productId)
        {
            if (!await _productReadOnlyRepositor.ExistProduct(productId)) throw new HahnApiException(ErrorCodeEnum.ProductNotFound);
            
            return await _productReadOnlyRepositor.GetProductPriceById(productId);
        }
    }
}
