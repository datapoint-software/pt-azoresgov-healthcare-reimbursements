﻿using Datapoint.UnitOfWork;
using System;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities
{
    public class Bank: IEntity
    {
        public long Id { get; set; } = default!;

        public Guid PublicId { get; set; } = default!;

        public Guid RowVersionId { get; set; } = default!;

        public string Name { get; set; } = default!;

        public string SwiftCode { get; set; } = default!;

        public string? SwiftLookupCode { get; set; } = default!;
    }
}