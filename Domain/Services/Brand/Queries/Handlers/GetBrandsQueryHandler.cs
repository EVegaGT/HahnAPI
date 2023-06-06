using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Models.Dtos;
using Domain.Models.Responses.Brand;
using Infrastructure.Repositories.ReadOnlyRepositories;
using MediatR;

namespace Domain.Services.Brand.Queries.Handlers
{
    public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, List<GetBrandResponse>>
    {
        private readonly IBrandReadOnlyRepository _brandReadOnlyRepository;
        private readonly IMapper _mapper;

        public GetBrandsQueryHandler(IBrandReadOnlyRepository brandReadOnlyRepository, IMapper mapper)
        {
            _brandReadOnlyRepository = brandReadOnlyRepository;
            _mapper = mapper;
        }
        public async Task<List<GetBrandResponse>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new HahnApiException(ErrorCodeEnum.BrandRequestIsRequired);

            try
            {
                var brands = _mapper.Map<List<BrandDto>>(await _brandReadOnlyRepository.GetBrands());
                return brands == null ? throw new HahnApiException(ErrorCodeEnum.BrandNotFound) : _mapper.Map<List<GetBrandResponse>>(brands);
            }
            catch (HahnApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HahnApiException(ErrorCodeEnum.InvalidRequest, $"Could not get brands. {ex.Message}");
            }
        }
    }
}
