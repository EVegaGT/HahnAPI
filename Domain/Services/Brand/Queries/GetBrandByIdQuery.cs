using Domain.Models.Dtos;
using Domain.Models.Responses.Brand;
using MediatR;

namespace Domain.Services.Brand.Queries
{
    public class GetBrandByIdQuery: IRequest<GetBrandResponse>
    {
        public Guid BrandId { get; set; }
        public GetBrandByIdQuery(Guid brandId) => BrandId = brandId;
    }
}
