using Domain.Models.Responses.Category;
using MediatR;

namespace Domain.Services.Category.Queries
{
    public class GetCategoriesQuery : IRequest<List<GetCategoryResponse>>
    {
    }
}
