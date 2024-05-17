using Datapoint.UnitOfWork;
using System;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities
{
    public sealed class Patient : IEntity
    {
        public long Id { get; set; } = default!;

        public Guid PublicId { get; set; } = default!;

        public Guid RowVersionId { get; set; } = default!;

        public string Number { get; set; } = default!;

        public string TaxNumber { get; set; } = default!;

        public string Name { get; set; } = default!;

        public DateTimeOffset Birth { get; set; } = default!;

        public DateTimeOffset? Death { get; set; } = default!;

        public string? FaxNumber { get; set; } = default!;

        public string? PhoneNumber { get; set; } = default!;

        public string? MobileNumber { get; set; } = default!;

        public string? EmailAddress { get; set; } = default!;

        public string PostalAddressArea { get; set; } = default!;

        public string PostalAddressAreaCode { get; set; } = default!;

        public string PostalAddressLine1 { get; set; } = default!;

        public string? PostalAddressLine2 { get; set; } = default!;

        public string? PostalAddressLine3 { get; set; } = default!;
    }
}
