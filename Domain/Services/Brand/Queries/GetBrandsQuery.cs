using Domain.Models.Responses.Brand;
using MediatR;

namespace Domain.Services.Brand.Queries
{
    public class GetBrandsQuery: IRequest<List<GetBrandResponse>>
    {
    }
}
