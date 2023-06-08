using Domain.Models.Responses.Category;
using MediatR;

namespace Domain.Services.Category.Queries
{
    public class GetCategoryByIdQuery : IRequest<GetCategoryResponse>
    {
        public Guid CategoryId { get; set; }
        public GetCategoryByIdQuery(Guid categoryId) => CategoryId = categoryId;
    }
}