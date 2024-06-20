﻿using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessSearch
{
    public sealed class MainProcessSearchFeaturePatientModel
    {
        public MainProcessSearchFeaturePatientModel(Guid id, Guid rowVersionId, Guid entityId, string number, string taxNumber, string name, DateTimeOffset? death)
        {
            Id = id;
            RowVersionId = rowVersionId;
            EntityId = entityId;
            Number = number;
            TaxNumber = taxNumber;
            Name = name;
            Death = death;
        }

        public Guid Id { get; }

        public Guid RowVersionId { get; }

        public Guid EntityId { get; }

        public string Number { get; }

        public string TaxNumber { get; }

        public string Name { get; }

        public DateTimeOffset? Death { get; }
    }
}
