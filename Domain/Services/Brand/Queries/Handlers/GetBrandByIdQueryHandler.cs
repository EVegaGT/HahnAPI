using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Models.Dtos;
using Domain.Models.Responses.Brand;
using Infrastructure.Repositories.ReadOnlyRepositories;
using MediatR;

namespace Domain.Services.Brand.Queries.Handlers
{
    public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, GetBrandResponse>
    {
        private readonly IBrandReadOnlyRepository _brandReadOnlyRepository;
        private readonly IMapper _mapper;

        public GetBrandByIdQueryHandler(IBrandReadOnlyRepository brandReadOnlyRepository, IMapper mapper)
        {
            _brandReadOnlyRepository = brandReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<GetBrandResponse> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new HahnApiException(ErrorCodeEnum.BrandRequestIsRequired);

            try
            {
                var brand = _mapper.Map<BrandDto>(await _brandReadOnlyRepository.GetBrandById(request.BrandId));
                return brand != null ? _mapper.Map<GetBrandResponse>(brand) : throw new HahnApiException(ErrorCodeEnum.BrandNotFound);
            }
            catch (HahnApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HahnApiException(ErrorCodeEnum.InvalidRequest, $"Could not get brand by ID {request.BrandId}. {ex.Message}");
            }
        }
    }
}
