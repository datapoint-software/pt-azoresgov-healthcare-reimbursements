using Datapoint.UnitOfWork;
using System;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities
{
    public class ProcessPatientFamilyIncomeStatement : IEntity
    {
        public long Id { get; set; } = default!;

        public Guid PublicId { get; set; } = default!;

        public Guid RowVersionId { get; set; } = default!;

        public Process Process { get; set; } = default!;

        public long ProcessId { get; } = default!;

        public int Year { get; set; } = default!;
        
        public string? AccessCode { get; set; } = default!;
        
        public int FamilyMemberCount { get; set; } = default!;
        
        public decimal FamilyIncome { get; set; } = default!;
    }
}