using AzoresGov.Healthcare.Reimbursements.Enumerations;
using Datapoint.UnitOfWork;
using System;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities
{
    public sealed class Patient : IEntity
    {
        public long Id { get; set; } = default!;

        public Guid PublicId { get; set; } = default!;

        public Guid RowVersionId { get; set; } = default!;

        public string Name { get; set; } = default!;

        public DateTimeOffset? Birth { get; set; } = default!;

        public Gender? Gender { get; set; } = default!;

        public string HealthNumber { get; set; } = default!;

        public string TaxNumber { get; set; } = default!;

        public string AddressLine1 { get; set; } = default!;

        public string? AddressLine2 { get; set; } = default!;

        public string? AddressLine3 { get; set; } = default!;

        public string? PostalCode { get; set; } = default!;

        public string? PostalCodeArea { get; set; } = default!;

        public string? EmailAddress { get; set; } = default!;

        public string? FaxNumber { get; set; } = default!;

        public string? MobileNumber { get; set; } = default!;

        public string? PhoneNumber { get; set; } = default!;

        public DateTimeOffset? Death { get; set; } = default!;
    }
}