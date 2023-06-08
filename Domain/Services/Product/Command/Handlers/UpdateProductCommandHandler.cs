using Domain.Models.Responses.Product;
using Infrastructure.Repositories.ReadOnlyRepositories;
using Infrastructure.Repositories;
using MediatR;
using Domain.Common.Exceptions;
using AutoMapper;
using Infrastructure.Models.Enums;
using Domain.Models.Dtos;

namespace Domain.Services.Product.Command.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductReadOnlyRepository _productReadOnlyRepository;
        private readonly IBrandReadOnlyRepository _brandReadOnlyRepository;
        private readonly ICategoryReadOnlyRepository _categoryReadOnlyRepository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository ProductRepository, IProductReadOnlyRepository productReadOnlyRepository, IBrandReadOnlyRepository brandReadOnlyRepository, ICategoryReadOnlyRepository categoryReadOnlyRepository, IMapper mapper)
        {
            _productRepository = ProductRepository;
            _productReadOnlyRepository = productReadOnlyRepository;
            _brandReadOnlyRepository = brandReadOnlyRepository;
            _categoryReadOnlyRepository = categoryReadOnlyRepository;
            _mapper = mapper;
        }
        public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new HahnApiException(ErrorCodeEnum.ProductRequestIsRequired);
            if (request.ProductId == Guid.Empty) throw new HahnApiException(ErrorCodeEnum.InvalidProductId);

            try
            {
                if (!await _brandReadOnlyRepository.ExistBrandById(request.BrandId)) throw new HahnApiException(ErrorCodeEnum.BrandNotFound);
                if (!await _categoryReadOnlyRepository.ExistCategoryById(request.CategoryId)) throw new HahnApiException(ErrorCodeEnum.BrandNotFound);

                var productDto = _mapper.Map<ProductDto>(await _productReadOnlyRepository.GetProductById(request.ProductId) ?? throw new HahnApiException(ErrorCodeEnum.ProductNotFound));
                productDto.Name = request.Name;
                productDto.ModifiedAt = DateTime.UtcNow;

                if (productDto.Quantity == 0)
                {
                    productDto.Status = ProductStatus.NoStock;
                }
                else if (productDto.Quantity > 0 && productDto.Status == ProductStatus.NoStock)
                {
                    productDto.Status = ProductStatus.Active;
                }

                await _productRepository.UpdateProduct(_mapper.Map<Infrastructure.Models.Product>(productDto));
                return _mapper.Map<ProductResponse>(productDto);
            }
            catch (Exception ex)
            {
                throw new HahnApiException(ErrorCodeEnum.InvalidRequest, $"Could not update the Product. {ex.Message}");
            }
        }
    }
}
