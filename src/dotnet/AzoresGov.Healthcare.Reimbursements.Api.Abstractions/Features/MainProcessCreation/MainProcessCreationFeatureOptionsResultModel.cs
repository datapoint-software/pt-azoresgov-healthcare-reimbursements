using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeatureOptionsResultModel
    {
        public MainProcessCreationFeatureOptionsResultModel(bool entitySelectionEnabled, IReadOnlyCollection<MainProcessCreationFeatureOptionsResultEntityModel>? entities, Guid? entityId)
        {
            EntitySelectionEnabled = entitySelectionEnabled;
            Entities = entities;
            EntityId = entityId;
        }

        public bool EntitySelectionEnabled { get; }

        public IReadOnlyCollection<MainProcessCreationFeatureOptionsResultEntityModel>? Entities { get; }

        public Guid? EntityId { get; }
    }
}
