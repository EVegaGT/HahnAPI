using Domain.Models.Responses.Product;
using Infrastructure.Repositories;
using MediatR;
using Domain.Common.Exceptions;
using Domain.Models.Dtos;
using AutoMapper;
using Infrastructure.Repositories.ReadOnlyRepositories;

namespace Domain.Services.Product.Command.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IBrandReadOnlyRepository _brandReadOnlyRepository;
        private readonly ICategoryReadOnlyRepository _categoryReadOnlyRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository ProductRepository, IBrandReadOnlyRepository brandReadOnlyRepository, ICategoryReadOnlyRepository categoryReadOnlyRepository,  IMapper mapper)
        {
            _productRepository = ProductRepository;
            _brandReadOnlyRepository = brandReadOnlyRepository;
            _categoryReadOnlyRepository = categoryReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new HahnApiException(ErrorCodeEnum.ProductRequestIsRequired);

            try
            {
                if (request.ProductId == Guid.Empty) request.ProductId = Guid.NewGuid();
                if (!await _brandReadOnlyRepository.ExistBrandById(request.BrandId)) throw new HahnApiException(ErrorCodeEnum.BrandNotFound);
                if (!await _categoryReadOnlyRepository.ExistCategoryById(request.CategoryId)) throw new HahnApiException(ErrorCodeEnum.CategoryNotFound);

                var productDto = _mapper.Map<ProductDto>(request);
                await _productRepository.CreateProduct(_mapper.Map<Infrastructure.Models.Product>(productDto));
                return _mapper.Map<ProductResponse>(productDto);
            }
            catch (Exception ex)
            {
                throw new HahnApiException(ErrorCodeEnum.InvalidRequest, $"Could not create Product. {ex.Message}");
            }
        }
    }
}
