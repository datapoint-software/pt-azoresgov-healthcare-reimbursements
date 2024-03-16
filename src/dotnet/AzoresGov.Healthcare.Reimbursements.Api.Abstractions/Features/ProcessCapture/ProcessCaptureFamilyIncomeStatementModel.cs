using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCaptureFamilyIncomeStatementModel
    {
        public ProcessCaptureFamilyIncomeStatementModel(Guid processId, Guid processRowVersionId, Guid? processPatientFamilyIncomeStatementRowVersionId, int year, string? accessCode, int familyMemberCount, decimal familyIncome)
        {
            ProcessId = processId;
            ProcessRowVersionId = processRowVersionId;
            ProcessPatientFamilyIncomeStatementRowVersionId = processPatientFamilyIncomeStatementRowVersionId;
            Year = year;
            AccessCode = accessCode;
            FamilyMemberCount = familyMemberCount;
            FamilyIncome = familyIncome;
        }

        public Guid ProcessId { get; }
        
        public Guid ProcessRowVersionId { get; }
        
        public Guid? ProcessPatientFamilyIncomeStatementRowVersionId { get; }
        
        public int Year { get; }
        
        public string? AccessCode { get; }
        
        public int FamilyMemberCount { get; }
        
        public decimal FamilyIncome { get; }
    }
}