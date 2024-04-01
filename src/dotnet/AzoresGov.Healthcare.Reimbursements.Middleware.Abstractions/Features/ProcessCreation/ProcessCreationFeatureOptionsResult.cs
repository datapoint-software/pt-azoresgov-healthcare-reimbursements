using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationFeatureOptionsResult
    {
        public bool EntitySelectionEnabled { get; }

        public IReadOnlyCollection<ProcessCreationFeatureOptionsResultEntity>? Entities { get; }

        public Guid? EntityId { get; }
    }
}
