using AzoresGov.Healthcare.Reimbursements.Enumerations;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationFeatureOptionsResultEntity
    {
        public ProcessCreationFeatureOptionsResultEntity(Guid id, Guid rowVersionId, string code, string name, EntityNature nature)
        {
            Id = id;
            RowVersionId = rowVersionId;
            Code = code;
            Name = name;
            Nature = nature;
        }

        public Guid Id { get; }

        public Guid RowVersionId { get; }

        public string Code { get; }

        public string Name { get; }

        public EntityNature Nature { get; }
    }
}