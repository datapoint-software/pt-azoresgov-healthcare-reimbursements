using AzoresGov.Healthcare.Reimbursements.Enumerations;
using Datapoint.UnitOfWork;
using System;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities
{
    public sealed class Entity : IEntity
    {
        public long Id { get; set; } = default!;

        public Guid PublicId { get; set; } = default!;

        public Guid RowVersionId { get; set; } = default!;

        public string Code { get; set; } = default!;

        public string Name { get; set; } = default!;

        public EntityNature Nature { get; set; } = default!;

        public string Node { get; set; } = default!;
    }
}
