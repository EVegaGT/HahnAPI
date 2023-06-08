using Domain.Models.Responses.Product;
using MediatR;

namespace Domain.Services.Product.Queries
{
    public class GetProductsQuery : IRequest<List<GetProductResponse>>
    {
    }
}
