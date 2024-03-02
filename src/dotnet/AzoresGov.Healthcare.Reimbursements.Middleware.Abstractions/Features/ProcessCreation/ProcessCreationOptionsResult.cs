using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationOptionsResult
    {
        public ProcessCreationOptionsResult(IReadOnlyCollection<ProcessCreationEntityResult>? entities, Guid? entityId)
        {
            Entities = entities;
            EntityId = entityId;
        }

        public IReadOnlyCollection<ProcessCreationEntityResult>? Entities { get; }

        public Guid? EntityId { get; }
    }
}