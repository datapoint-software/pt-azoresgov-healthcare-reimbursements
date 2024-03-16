using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public class ProcessCaptureFamilyIncomeStatementDeleteResultModel
    {
        public ProcessCaptureFamilyIncomeStatementDeleteResultModel(Guid processRowVersionId)
        {
            ProcessRowVersionId = processRowVersionId;
        }

        public Guid ProcessRowVersionId { get; }
    }
}