﻿using AzoresGov.Healthcare.Reimbursements.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessSearch
{
    public sealed class MainProcessSearchFeatureEntityModel
    {
        public MainProcessSearchFeatureEntityModel(Guid id, Guid rowVersionId, Guid? parentEntityId, string code, string name, EntityNature nature)
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
