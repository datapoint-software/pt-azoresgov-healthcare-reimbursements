using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureFamilyIncomeStatementDeleteCommandValidator : AbstractValidator<ProcessCaptureFamilyIncomeStatementDeleteCommand>
    {
        public ProcessCaptureFamilyIncomeStatementDeleteCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty();

            RuleFor(c => c.ProcessId)
                .NotEmpty();

            RuleFor(c => c.ProcessRowVersionId)
                .NotEmpty();

            RuleFor(c => c.ProcessPatientFamilyIncomeStatementRowVersionId)
                .NotEmpty();
        }
    }
}