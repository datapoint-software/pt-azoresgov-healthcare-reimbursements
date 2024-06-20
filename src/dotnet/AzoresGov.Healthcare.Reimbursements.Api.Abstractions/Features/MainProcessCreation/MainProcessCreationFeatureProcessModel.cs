using AzoresGov.Healthcare.Reimbursements.Enumerations;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeatureProcessModel
    {
        public MainProcessCreationFeatureProcessModel(Guid id, Guid rowVersionId, string number, ProcessStatus status, DateTimeOffset creation)
        {
            Id = id;
            RowVersionId = rowVersionId;
            Number = number;
            Status = status;
            Creation = creation;
        }

        public Guid Id { get; }

        public Guid RowVersionId { get; }

        public string Number { get; }

        public ProcessStatus Status { get; }

        public DateTimeOffset Creation { get; }
    }
}