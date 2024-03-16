using Datapoint.UnitOfWork;
using System;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities
{
    public sealed class ProcessConfiguration : IEntity
    {
        public long Id { get; set; } = default!;

        public Guid PublicId { get; set; } = default!;

        public Guid RowVersionId { get; set; } = default!;

        public Process Process { get; set; } = default!;

        public long ProcessId { get; set; } = default!;

        public bool MachadoJosephEnabled { get; set; } = default!;
        
        public bool DocumentIssueDateBypassEnabled { get; set; } = default!;
        
        public bool ReimbursementLimitBypassEnabled { get; set; } = default!;

        public bool UnemploymentEnabled { get; set; } = default!;
    }
}