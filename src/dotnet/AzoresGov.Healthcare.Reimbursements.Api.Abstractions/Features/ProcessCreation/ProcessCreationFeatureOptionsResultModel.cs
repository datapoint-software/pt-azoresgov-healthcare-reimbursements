using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCreation
{
    public sealed class ProcessCreationFeatureOptionsResultModel
    {
        public ProcessCreationFeatureOptionsResultModel(bool entitySelectionEnabled, IReadOnlyCollection<ProcessCreationFeatureOptionsResultEntityModel>? entities, Guid? entityId)
        {
            EntitySelectionEnabled = entitySelectionEnabled;
            Entities = entities;
            EntityId = entityId;
        }

        public bool EntitySelectionEnabled { get; }

        public IReadOnlyCollection<ProcessCreationFeatureOptionsResultEntityModel>? Entities { get; }

        public Guid? EntityId { get; }
    }
}
