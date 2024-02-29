using AzoresGov.Healthcare.Reimbursements.Enumerations;
using System;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Workers.Features.ProcessCreation
{
    public sealed class ProcessCreationEntitySearchResult
    {
        public ProcessCreationEntitySearchResult(long id, Guid publicId, Guid rowVersionId, long parentEntityId, string code, string name, EntityNature nature)
        {
            Id = id;
            PublicId = publicId;
            RowVersionId = rowVersionId;
            ParentEntityId = parentEntityId;
            Code = code;
            Name = name;
            Nature = nature;
        }

        public long Id { get; }
        
        public Guid PublicId { get; }
        
        public Guid RowVersionId { get; }
        
        public long ParentEntityId { get; }
        
        public string Code { get; }
        
        public string Name { get; }
        
        public EntityNature Nature { get; }
    }
}