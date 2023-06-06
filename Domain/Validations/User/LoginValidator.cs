using Domain.Models.Requests.User;
using FluentValidation;

namespace Domain.Validations.User
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(brand => brand.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(150).WithMessage("Email must be less than 150 characters.")
                .EmailAddress().WithMessage("Email must has valid format");
            RuleFor(brand => brand.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(15).WithMessage("Password must be less than 15 characters.");
        }
    }
}
