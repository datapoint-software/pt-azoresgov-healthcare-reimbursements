﻿using AzoresGov.Healthcare.Reimbursements.Enumerations;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeatureEntity
    {
        public MainProcessCreationFeatureEntity(Guid id, Guid rowVersionId, Guid? parentEntityId, string code, string name, EntityNature nature)
        {
            Id = id;
            RowVersionId = rowVersionId;
            ParentEntityId = parentEntityId;
            Code = code;
            Name = name;
            Nature = nature;
        }

        public Guid Id { get; }

        public Guid RowVersionId { get; }

        public Guid? ParentEntityId { get; }

        public string Code { get; }

        public string Name { get; }

        public EntityNature Nature { get; }
    }
}