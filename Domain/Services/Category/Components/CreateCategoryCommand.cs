using Domain.Models.Responses.Category;
using MediatR;

namespace Domain.Services.Category.Components
{
    public class CreateCategoryCommand : IRequest<CategoryResponse>
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
