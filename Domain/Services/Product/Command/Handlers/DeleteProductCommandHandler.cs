using Infrastructure.Repositories.ReadOnlyRepositories;
using Infrastructure.Repositories;
using MediatR;
using Domain.Common.Exceptions;
using Infrastructure.Models.Enums;

namespace Domain.Services.Product.Command.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductReadOnlyRepository _productReadOnlyRepository;

        public DeleteProductCommandHandler(IProductRepository ProductRepository, IProductReadOnlyRepository productReadOnlyRepository)
        {
            _productRepository = ProductRepository;
            _productReadOnlyRepository = productReadOnlyRepository;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new HahnApiException(ErrorCodeEnum.ProductRequestIsRequired);
            if (request.ProductId == Guid.Empty) throw new HahnApiException(ErrorCodeEnum.InvalidProductId);

            try
            {
                var product = await _productReadOnlyRepository.GetProductById(request.ProductId) ?? throw new HahnApiException(ErrorCodeEnum.ProductNotFound);
                product.Status = ProductStatus.Inactive;
                product.ModifiedAt = DateTime.UtcNow;
                await _productRepository.UpdateProduct(product);
                return true;
            }
            catch (Exception ex)
            {
                throw new HahnApiException(ErrorCodeEnum.InvalidRequest, $"Could not delete the Product. {ex.Message}");
            }
        }
    }
}
