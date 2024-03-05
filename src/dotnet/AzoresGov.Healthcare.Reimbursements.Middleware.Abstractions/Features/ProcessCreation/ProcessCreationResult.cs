using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationResult
    {
        public ProcessCreationResult(Guid id, Guid rowVersionId, string number)
        {
            Id = id;
            RowVersionId = rowVersionId;
            Number = number;
        }

        public Guid Id { get; }
        
        public Guid RowVersionId { get; }
        
        public string Number { get; }
    }
}