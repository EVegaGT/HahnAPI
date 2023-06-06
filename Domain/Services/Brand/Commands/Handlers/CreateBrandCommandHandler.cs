using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Models.Dtos;
using Domain.Models.Responses.Brand;
using Infrastructure.Repositories;
using MediatR;

namespace Domain.Services.Brand.Commands.Handlers
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, BrandResponse>
    {
        private readonly IBrandRepository _BrandRepository;
        private readonly IMapper _mapper;

        public CreateBrandCommandHandler(IBrandRepository BrandRepository, IMapper mapper)
        {
            _BrandRepository = BrandRepository;
            _mapper = mapper;
        }

        public async Task<BrandResponse> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new HahnApiException(ErrorCodeEnum.BrandRequestIsRequired);

            try
            {
                if (request.BrandId == Guid.Empty) request.BrandId = Guid.NewGuid();

                var brandDto = _mapper.Map<BrandDto>(request);
                await _BrandRepository.CreateBrand(_mapper.Map<Infrastructure.Models.Brand>(brandDto));
                return new BrandResponse(brandDto.BrandId);
            }
            catch (Exception ex)
            {
                throw new HahnApiException(ErrorCodeEnum.InvalidRequest, $"Could not create Brand. {ex.Message}");
            }
        }
    }
}
