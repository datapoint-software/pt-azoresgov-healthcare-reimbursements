using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(c => c.EmailAddress)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(c => c.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(4096);

            RuleFor(c => c.Persistent);
        }
    }
}
