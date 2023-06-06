using Infrastructure.Repositories.ReadOnlyRepositories;
using Infrastructure.Repositories;
using MediatR;
using Domain.Common.Exceptions;

namespace Domain.Services.Brand.Commands.Handlers
{
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, bool>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IBrandReadOnlyRepository _brandReadOnlyRepository;

        public DeleteBrandCommandHandler(IBrandRepository BrandRepository, IBrandReadOnlyRepository brandReadOnlyRepository)
        {
            _brandRepository = BrandRepository;
            _brandReadOnlyRepository = brandReadOnlyRepository;
        }

        public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new HahnApiException(ErrorCodeEnum.BrandRequestIsRequired);
            if (request.BrandId == Guid.Empty) throw new HahnApiException(ErrorCodeEnum.InvalidBrandId);

            try
            {
                var brand = await _brandReadOnlyRepository.GetBrandById(request.BrandId) ?? throw new HahnApiException(ErrorCodeEnum.BrandNotFound);
                brand.IsDeleted = true;
                await _brandRepository.UpdateBrand(brand);
                return true;
            }
            catch (Exception ex)
            {
                throw new HahnApiException(ErrorCodeEnum.InvalidRequest, $"Could not delete the Brand. {ex.Message}");
            }
        }
    }
}