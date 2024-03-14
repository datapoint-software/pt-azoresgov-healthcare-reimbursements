using AzoresGov.Healthcare.Reimbursements.Enumerations;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureOptionsProcessResult
    {
        public ProcessCaptureOptionsProcessResult(Guid id, Guid rowVersionId, string number, ProcessStatus status)
        {
            Id = id;
            RowVersionId = rowVersionId;
            Number = number;
            Status = status;
        }
        
        public Guid Id { get; }

        public Guid RowVersionId { get; }
        
        public string Number { get; }
        
        public ProcessStatus Status { get; }
    }
}