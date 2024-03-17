using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureBankQueryValidator : AbstractValidator<ProcessCaptureBankQuery>
    {
        public ProcessCaptureBankQueryValidator()
        {
            RuleFor(c => c.Iban)
                .Iban()
                .NotEmpty();
        }
    }
}