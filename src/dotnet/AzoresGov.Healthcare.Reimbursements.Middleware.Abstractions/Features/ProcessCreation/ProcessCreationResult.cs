using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationResult
    {
        public ProcessCreationResult(Guid id, Guid rowVersionId)
        {
            Id = id;
            RowVersionId = rowVersionId;
        }

        public Guid Id { get; }
        
        public Guid RowVersionId { get; }
    }
}