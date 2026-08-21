using FluentValidation;

namespace TrendSense.Application.Features.Auth.Commands.Register
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator() 
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(50);
        }
    }
}
