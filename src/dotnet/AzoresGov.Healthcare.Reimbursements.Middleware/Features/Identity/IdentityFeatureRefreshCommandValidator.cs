using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity
{
    public sealed class IdentityFeatureRefreshCommandValidator : AbstractValidator<IdentityFeatureRefreshCommand>
    {
        public IdentityFeatureRefreshCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty();

            RuleFor(c => c.UserSessionId)
                .NotEmpty();
        }
    }
}
