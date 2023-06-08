using MediatR;

namespace Domain.Services.Category.Components
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public Guid CategoryId { get; set; }

        public DeleteCategoryCommand(Guid categoryId) => CategoryId = categoryId;
    }
}
