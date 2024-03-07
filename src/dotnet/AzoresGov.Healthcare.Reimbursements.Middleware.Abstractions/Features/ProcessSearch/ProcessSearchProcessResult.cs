using AzoresGov.Healthcare.Reimbursements.Enumerations;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessSearch
{
    public sealed class ProcessSearchProcessResult
    {
        public ProcessSearchProcessResult(Guid id, Guid entityId, Guid patientId, string number, ProcessStatus status, DateTimeOffset creation, DateTimeOffset? expiration, DateTimeOffset touch)
        {
            Id = id;
            EntityId = entityId;
            PatientId = patientId;
            Number = number;
            Status = status;
            Creation = creation;
            Expiration = expiration;
            Touch = touch;
        }

        public Guid Id { get; }
        
        public Guid EntityId { get; }
        
        public Guid PatientId { get; }
        
        public string Number { get; }
        
        public ProcessStatus Status { get; }
        
        public DateTimeOffset Creation { get; }
        
        public DateTimeOffset? Expiration { get; }
        
        public DateTimeOffset Touch { get; }
    }
}