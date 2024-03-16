using Datapoint.UnitOfWork;
using System;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities
{
    public class PatientFamilyIncomeStatement : IEntity
    {
        public long Id { get; set; } = default!;

        public Guid PublicId { get; set; } = default!;

        public Guid RowVersionId { get; set; } = default!;

        public Patient Patient { get; set; } = default!;

        public long PatientId { get; } = default!;

        public int Year { get; set; } = default!;
        
        public string? AccessCode { get; set; } = default!;
        
        public int FamilyMemberCount { get; set; } = default!;
        
        public decimal FamilyIncome { get; set; } = default!;
    }
}