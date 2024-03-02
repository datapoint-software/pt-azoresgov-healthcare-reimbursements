using AzoresGov.Healthcare.Reimbursements.Enumerations;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationPatientResult
    {
        public ProcessCreationPatientResult(Guid id, Guid rowVersionId, string name, DateTimeOffset? birth, Gender? gender, string healthNumber, string taxNumber, DateTimeOffset? death)
        {
            Id = id;
            RowVersionId = rowVersionId;
            Name = name;
            Birth = birth;
            Gender = gender;
            HealthNumber = healthNumber;
            TaxNumber = taxNumber;
            Death = death;
        }

        public Guid Id { get; }
        
        public Guid RowVersionId { get; }
        
        public string Name { get; }
        
        public DateTimeOffset? Birth { get; }
        
        public Gender? Gender { get; }
        
        public string HealthNumber { get; }
        
        public string TaxNumber { get; }
        
        public DateTimeOffset? Death { get; }
    }
}