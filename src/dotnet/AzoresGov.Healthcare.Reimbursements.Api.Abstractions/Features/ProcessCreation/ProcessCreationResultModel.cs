using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCreation
{
    public sealed class ProcessCreationResultModel
    {
        public ProcessCreationResultModel(Guid id, Guid rowVersionId, string number)
        {
            Id = id;
            RowVersionId = rowVersionId;
            Number = number;
        }

        public Guid Id { get; }
        
        public Guid RowVersionId { get; }
        
        public string Number { get; }
    }
}