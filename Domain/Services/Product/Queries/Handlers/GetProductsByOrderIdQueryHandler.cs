using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Models.Dtos;
using Domain.Models.Responses.Product;
using Infrastructure.Repositories.ReadOnlyRepositories;
using MediatR;

namespace Domain.Services.Product.Queries.Handlers
{
    public class GetProductsByOrderIdQueryHandler : IRequestHandler<GetProductsByOrderIdQuery, List<GetProductResponse>>
    {
        private readonly IProductReadOnlyRepository _productReadOnlyRepository;
        private readonly IMapper _mapper;

        public GetProductsByOrderIdQueryHandler(IProductReadOnlyRepository productReadOnlyRepository, IMapper mapper)
        {
            _productReadOnlyRepository = productReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<List<GetProductResponse>> Handle(GetProductsByOrderIdQuery request, CancellationToken cancellationToken)
        {

            if (request == null) throw new HahnApiException(ErrorCodeEnum.ProductRequestIsRequired);
            if (request.OrderId == Guid.Empty) throw new HahnApiException(ErrorCodeEnum.InvalidOrderId);

            try
            {
                var products = _mapper.Map<List<ProductDto>>(await _productReadOnlyRepository.GetProductsOrder(request.OrderId));
                return products == null ? throw new HahnApiException(ErrorCodeEnum.ProductNotFound) : _mapper.Map<List<GetProductResponse>>(products);
            }
            catch (HahnApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HahnApiException(ErrorCodeEnum.InvalidRequest, $"Could not get products. {ex.Message}");
            }
        }
    }
}
