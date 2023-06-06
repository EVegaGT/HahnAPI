using Domain.Common.Exceptions;
using Domain.Models.Responses.Brand;
using Domain.Services.Brand.Commands.Handlers;
using Infrastructure.Repositories;
using Infrastructure.Repositories.ReadOnlyRepositories;
using MediatR;

namespace Domain.Services.Brand.Commands
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, BrandResponse>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IBrandReadOnlyRepository _brandReadOnlyRepository;

        public UpdateBrandCommandHandler(IBrandRepository BrandRepository, IBrandReadOnlyRepository brandReadOnlyRepository)
        {
            _brandRepository = BrandRepository;
            _brandReadOnlyRepository = brandReadOnlyRepository;
        }

        public async Task<BrandResponse> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new HahnApiException(ErrorCodeEnum.BrandRequestIsRequired);
            if (request.BrandId == Guid.Empty) throw new HahnApiException(ErrorCodeEnum.InvalidBrandId);

            try
            {
                var brand =  await _brandReadOnlyRepository.GetBrandById(request.BrandId) ?? throw new HahnApiException(ErrorCodeEnum.BrandNotFound);
                brand.Name = request.Name;
                await _brandRepository.UpdateBrand(brand);
                return new BrandResponse(brand.BrandId);
            }
            catch (Exception ex)
            {
                throw new HahnApiException(ErrorCodeEnum.InvalidRequest, $"Could not update the Brand. {ex.Message}");
            }
        }
    }
}
