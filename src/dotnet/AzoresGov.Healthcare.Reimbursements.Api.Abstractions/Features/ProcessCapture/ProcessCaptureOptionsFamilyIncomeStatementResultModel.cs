using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCaptureOptionsFamilyIncomeStatementResultModel
    {
        public ProcessCaptureOptionsFamilyIncomeStatementResultModel(Guid? rowVersionId, int year, string? accessCode, int familyMemberCount, decimal familyIncome)
        {
            RowVersionId = rowVersionId;
            Year = year;
            AccessCode = accessCode;
            FamilyMemberCount = familyMemberCount;
            FamilyIncome = familyIncome;
        }

        public Guid? RowVersionId { get; }
        
        public int Year { get; }
        
        public string? AccessCode { get; }
        
        public int FamilyMemberCount { get; }
        
        public decimal FamilyIncome { get; }
    }
}