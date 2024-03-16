using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureFamilyIncomeStatementCommand : Command<ProcessCaptureFamilyIncomeStatementResult>
    {
        public ProcessCaptureFamilyIncomeStatementCommand(Guid userId, Guid processId, Guid processRowVersionId, Guid? processPatientFamilyIncomeStatementRowVersionId, int year, string? accessCode, int familyMemberCount, decimal familyIncome)
        {
            UserId = userId;
            ProcessId = processId;
            ProcessRowVersionId = processRowVersionId;
            ProcessPatientFamilyIncomeStatementRowVersionId = processPatientFamilyIncomeStatementRowVersionId;
            Year = year;
            AccessCode = accessCode;
            FamilyMemberCount = familyMemberCount;
            FamilyIncome = familyIncome;
        }

        public Guid UserId { get; }
        
        public Guid ProcessId { get; }
        
        public Guid ProcessRowVersionId { get; }
        
        public Guid? ProcessPatientFamilyIncomeStatementRowVersionId { get; }
        
        public int Year { get; }
        
        public string? AccessCode { get; }
        
        public int FamilyMemberCount { get; }
        
        public decimal FamilyIncome { get; }
    }
}