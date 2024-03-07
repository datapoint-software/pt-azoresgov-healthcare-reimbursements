using AzoresGov.Healthcare.Reimbursements.Enumerations;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessSearch
{
    public sealed class ProcessSearchPatientResultModel
    {
        public ProcessSearchPatientResultModel(Guid id, string name, Gender? gender, string healthNumber, string taxNumber, DateTimeOffset? birth, DateTimeOffset? death)
        {
            Id = id;
            Name = name;
            Gender = gender;
            HealthNumber = healthNumber;
            TaxNumber = taxNumber;
            Birth = birth;
            Death = death;
        }

        public Guid Id { get; }
        
        public string Name { get; }
        
        public Gender? Gender { get; }
        
        public string HealthNumber { get; }
        
        public string TaxNumber { get; }
        
        public DateTimeOffset? Birth { get; }
        
        public DateTimeOffset? Death { get; }
    }
}