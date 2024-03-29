﻿using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureOptionsFamilyIncomeStatementResult
    {
        public ProcessCaptureOptionsFamilyIncomeStatementResult(Guid? rowVersionId, int year, string? accessCode, int familyMemberCount, decimal familyIncome)
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