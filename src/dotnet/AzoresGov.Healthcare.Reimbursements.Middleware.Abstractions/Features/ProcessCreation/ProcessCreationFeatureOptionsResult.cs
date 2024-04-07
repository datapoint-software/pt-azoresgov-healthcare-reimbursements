using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationFeatureOptionsResult
    {
        public ProcessCreationFeatureOptionsResult(bool entitySelectionEnabled, IReadOnlyCollection<ProcessCreationFeatureOptionsResultEntity>? entities, Guid? entityId)
        {
            EntitySelectionEnabled = entitySelectionEnabled;
            Entities = entities;
            EntityId = entityId;
        }

        public bool EntitySelectionEnabled { get; }

        public IReadOnlyCollection<ProcessCreationFeatureOptionsResultEntity>? Entities { get; }

        public Guid? EntityId { get; }
    }
}
