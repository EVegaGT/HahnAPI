using Domain.Models.Requests.User;
using FluentValidation;

namespace Domain.Validations.User
{
    public class RegisterValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterValidator()
        {
            RuleFor(brand => brand.FirstName)
               .NotEmpty().WithMessage("FirstName is required.")
               .MaximumLength(50).WithMessage("FirstName must be less than 50 characters.");
            RuleFor(brand => brand.LastName)
               .NotEmpty().WithMessage("LastName is required.")
               .MaximumLength(50).WithMessage("LastName must be less than 50 characters.");
            RuleFor(brand => brand.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(150).WithMessage("Email must be less than 150 characters.")
                .EmailAddress().WithMessage("Email must has valid format");
            RuleFor(brand => brand.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(15).WithMessage("Password must be less than 15 characters.")
                .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$").WithMessage("Password must has minimum 8 characters in length, at least one uppercase letter, one lowercase letter and one digit.");
        }
    }
}
