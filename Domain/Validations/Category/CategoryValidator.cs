using Domain.Models.Requests.Category;
using FluentValidation;

namespace Domain.Validations.Category
{
    public class CategoryValidator : AbstractValidator<CategoryRequest>
    {
        public CategoryValidator()
        {
            RuleFor(category => category.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name must be less than 50 characters.");
        }
    }
}
