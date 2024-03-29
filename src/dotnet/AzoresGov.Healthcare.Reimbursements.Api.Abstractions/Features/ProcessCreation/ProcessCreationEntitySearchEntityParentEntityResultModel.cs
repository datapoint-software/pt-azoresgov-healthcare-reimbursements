﻿using AzoresGov.Healthcare.Reimbursements.Enumerations;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCreation
{
    public class ProcessCreationEntitySearchEntityParentEntityResultModel
    {
        public ProcessCreationEntitySearchEntityParentEntityResultModel(Guid id, Guid rowVersionId, string code, string name, EntityNature nature)
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