using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Models.Dtos;
using Domain.Models.Responses.Product;
using Infrastructure.Repositories.ReadOnlyRepositories;
using MediatR;

namespace Domain.Services.Product.Queries.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductResponse>
    {
        private readonly IProductReadOnlyRepository _productReadOnlyRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IProductReadOnlyRepository productReadOnlyRepository, IMapper mapper)
        {
            _productReadOnlyRepository = productReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<GetProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new HahnApiException(ErrorCodeEnum.ProductRequestIsRequired);

            try
            {
                var product = _mapper.Map<ProductDto>(await _productReadOnlyRepository.GetProductById(request.ProductId));
                return product != null ? _mapper.Map<GetProductResponse>(product) : throw new HahnApiException(ErrorCodeEnum.ProductNotFound);
            }
            catch (HahnApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HahnApiException(ErrorCodeEnum.InvalidRequest, $"Could not get product by ID {request.ProductId}. {ex.Message}");
            }
        }
    }
}
