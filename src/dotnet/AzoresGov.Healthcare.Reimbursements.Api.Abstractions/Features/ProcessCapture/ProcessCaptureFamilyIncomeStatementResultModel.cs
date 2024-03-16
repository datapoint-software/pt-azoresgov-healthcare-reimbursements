using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCaptureFamilyIncomeStatementResultModel
    {
        public ProcessCaptureFamilyIncomeStatementResultModel(Guid processRowVersionId, Guid processPatientFamilyIncomeStatementRowVersionId)
        {
            ProcessRowVersionId = processRowVersionId;
            ProcessPatientFamilyIncomeStatementRowVersionId = processPatientFamilyIncomeStatementRowVersionId;
        }

        public Guid ProcessRowVersionId { get; }
        
        public Guid ProcessPatientFamilyIncomeStatementRowVersionId { get; }
    }
}