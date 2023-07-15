using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(x => x.UserAgent)
                .NotEmpty()
                .MaximumLength(4096);

            RuleFor(x => x.NetworkAddress)
                .NotEmpty();

            RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(256);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MaximumLength(1024);

            RuleFor(x => x.Persistent);
        }
    }
}
