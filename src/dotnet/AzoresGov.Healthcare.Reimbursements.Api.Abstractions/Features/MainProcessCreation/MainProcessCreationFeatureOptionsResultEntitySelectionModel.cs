using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeatureOptionsResultEntitySelectionModel
    {
        public MainProcessCreationFeatureOptionsResultEntitySelectionModel(Guid entityId, IReadOnlyCollection<MainProcessCreationFeatureEntityModel> entities)
        {
            EntityId = entityId;
            Entities = entities;
        }

        public Guid EntityId { get; }

        public IReadOnlyCollection<MainProcessCreationFeatureEntityModel> Entities { get; }
    }
}