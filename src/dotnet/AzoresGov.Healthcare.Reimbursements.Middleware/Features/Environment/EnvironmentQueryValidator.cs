using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Environment
{
    public sealed class EnvironmentQueryValidator : AbstractValidator<EnvironmentQuery>
    {
        public EnvironmentQueryValidator()
        {
        }
    }
}
