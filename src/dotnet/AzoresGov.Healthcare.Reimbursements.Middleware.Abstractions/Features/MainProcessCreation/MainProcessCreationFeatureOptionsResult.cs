using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeatureOptionsResult
    {
        public MainProcessCreationFeatureOptionsResult(bool entitySelectionEnabled, IReadOnlyCollection<MainProcessCreationFeatureOptionsResultEntity>? entities, Guid? entityId)
        {
            EntitySelectionEnabled = entitySelectionEnabled;
            Entities = entities;
            EntityId = entityId;
        }

        public bool EntitySelectionEnabled { get; }

        public IReadOnlyCollection<MainProcessCreationFeatureOptionsResultEntity>? Entities { get; }

        public Guid? EntityId { get; }
    }
}
