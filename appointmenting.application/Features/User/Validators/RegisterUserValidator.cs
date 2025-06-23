using appointmenting.Dtos.User;
using FluentValidation;

namespace appointmenting.Features.User.Validators;

public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
{
    public UserRegisterValidator()
    {
        RuleFor(r => r.email)
            .NotEmpty().WithMessage("Email is a required Field")
            .EmailAddress().WithMessage("Not a valid Email");
        RuleFor(r => r.username)
            .NotEmpty().WithMessage("Username is a required Field")
            .MinimumLength(6).WithMessage("Username must be at least 6 Characters");
        RuleFor(r => r.password)
            .NotEmpty().WithMessage("Password is a required Field")
            .MinimumLength(8).WithMessage("Password must be at least 8 Characters")
            .MaximumLength(32).WithMessage("Password can only be 32 Characters");
        RuleFor(r => r.passwordConfirmation)
            .NotEmpty().WithMessage("PasswordConfirmation is a required Field")
            .Equal(r => r.password).WithMessage("Passwords do not match");
    }
}
