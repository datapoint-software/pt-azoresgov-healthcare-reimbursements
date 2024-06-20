using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeatureOptionsResultEntitySelection
    {
        public MainProcessCreationFeatureOptionsResultEntitySelection(Guid entityId, IReadOnlyCollection<MainProcessCreationFeatureEntity> entities)
        {
            EntityId = entityId;
            Entities = entities;
        }

        public Guid EntityId { get; }

        public IReadOnlyCollection<MainProcessCreationFeatureEntity> Entities { get; }
    }
}