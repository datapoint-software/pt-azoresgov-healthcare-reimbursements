using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureFamilyIncomeStatementResult
    {
        public ProcessCaptureFamilyIncomeStatementResult(Guid processRowVersionId, Guid processPatientFamilyIncomeStatementRowVersionId)
        {
            ProcessRowVersionId = processRowVersionId;
            ProcessPatientFamilyIncomeStatementRowVersionId = processPatientFamilyIncomeStatementRowVersionId;
        }

        public Guid ProcessRowVersionId { get; }
        
        public Guid ProcessPatientFamilyIncomeStatementRowVersionId { get; }
    }
}