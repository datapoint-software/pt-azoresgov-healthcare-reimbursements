using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCreation
{
    public sealed class ProcessCreationOptionsResultModel
    {
        public ProcessCreationOptionsResultModel(IReadOnlyCollection<ProcessCreationEntityResultModel>? entities, Guid? entityId)
        {
            Entities = entities;
            EntityId = entityId;
        }

        public IReadOnlyCollection<ProcessCreationEntityResultModel>? Entities { get; }

        public Guid? EntityId { get; }
    }
}