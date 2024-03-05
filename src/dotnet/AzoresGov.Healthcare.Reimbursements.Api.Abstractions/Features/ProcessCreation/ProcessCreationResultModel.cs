using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCreation
{
    public sealed class ProcessCreationResultModel
    {
        public ProcessCreationResultModel(Guid id, Guid rowVersionId)
        {
            Id = id;
            RowVersionId = rowVersionId;
        }

        public Guid Id { get; }
        
        public Guid RowVersionId { get; }
    }
}