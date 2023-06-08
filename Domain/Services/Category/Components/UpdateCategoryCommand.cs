using Domain.Models.Responses.Category;
using MediatR;

namespace Domain.Services.Category.Components
{
    public class UpdateCategoryCommand : IRequest<CategoryResponse>
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;

        public UpdateCategoryCommand(Guid categoryId, string name)
        {
            CategoryId = categoryId;
            Name = name;
        }
    }
}
