using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureFamilyIncomeStatementDeleteCommand : Command<ProcessCaptureFamilyIncomeStatementDeleteResult>
    {
        public ProcessCaptureFamilyIncomeStatementDeleteCommand(Guid userId, Guid processId, Guid processRowVersionId, Guid processPatientFamilyIncomeStatementRowVersionId)
        {
            UserId = userId;
            ProcessId = processId;
            ProcessRowVersionId = processRowVersionId;
            ProcessPatientFamilyIncomeStatementRowVersionId = processPatientFamilyIncomeStatementRowVersionId;
        }

        public Guid UserId { get; }
        
        public Guid ProcessId { get; }
        
        public Guid ProcessRowVersionId { get; }
        
        public Guid ProcessPatientFamilyIncomeStatementRowVersionId { get; }
    }
}