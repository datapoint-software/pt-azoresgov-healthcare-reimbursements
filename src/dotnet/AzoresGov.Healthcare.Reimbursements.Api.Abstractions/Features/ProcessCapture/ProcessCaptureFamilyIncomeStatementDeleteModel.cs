using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCaptureFamilyIncomeStatementDeleteModel
    {
        public ProcessCaptureFamilyIncomeStatementDeleteModel(Guid processId, Guid processRowVersionId, Guid processPatientFamilyIncomeStatementRowVersionId)
        {
            ProcessId = processId;
            ProcessRowVersionId = processRowVersionId;
            ProcessPatientFamilyIncomeStatementRowVersionId = processPatientFamilyIncomeStatementRowVersionId;
        }

        public Guid ProcessId { get; }
        
        public Guid ProcessRowVersionId { get; }
        
        public Guid ProcessPatientFamilyIncomeStatementRowVersionId { get; }
    }
}