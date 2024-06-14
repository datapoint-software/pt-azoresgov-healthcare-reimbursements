using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCreationConfirmation
{
    public sealed class MainProcessCreationConfirmationFeatureConfirmResultModel
    {
        public MainProcessCreationConfirmationFeatureConfirmResultModel(Guid processId, Guid processRowVersionId)
        {
            ProcessId = processId;
            ProcessRowVersionId = processRowVersionId;
        }

        public Guid ProcessId { get; }

        public Guid ProcessRowVersionId { get; }
    }
}