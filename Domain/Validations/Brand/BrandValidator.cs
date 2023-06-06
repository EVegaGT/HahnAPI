using Domain.Models.Requests.Brand;
using FluentValidation;

namespace Domain.Validations.Brand
{
    public class BrandValidator : AbstractValidator<BrandRequest>
    {
        public BrandValidator()
        {
            RuleFor(brand => brand.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name must be less than 50 characters.");
        }
    }
}
