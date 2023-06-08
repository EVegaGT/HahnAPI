using Domain.Models.Requests;
using FluentValidation;


namespace Domain.Validations.Product
{
    public class ProductValidator : AbstractValidator<ProductRequest>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Name)
               .NotEmpty().NotNull().WithMessage("Name is required.")
               .MaximumLength(100).WithMessage("FirstName must be less than 100 characters.");
            RuleFor(brand => brand.Description)
               .NotEmpty().NotNull().WithMessage("Description is required.")
               .MaximumLength(500).WithMessage("LastName must be less than 500 characters.");
            RuleFor(brand => brand.Quantity)
                .NotEmpty().NotNull().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity should be greater than 0.");
            RuleFor(brand => brand.Price)
             .NotEmpty().NotNull().WithMessage("Price is required.")
             .GreaterThan(0).WithMessage("Price should be greater than 0.");
            RuleFor(product => product.CategoryId)
               .NotEmpty().NotNull().WithMessage("CategoryId is required.");
            RuleFor(product => product.BrandId)
              .NotEmpty().NotNull().WithMessage("BrandId is required.");
        }
    }
}
