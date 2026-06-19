using Alumni.DTOS;
using FluentValidation;

namespace Alumni.Validations
{
    public class UserValidator:AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("User name is required");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid Email");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password field required")
                .MinimumLength(4).WithMessage("Password must be at least 8");

            RuleFor(x => x.Role)
                .IsInEnum()
                .WithMessage("Please select a valid Role");

        }
    }
}
