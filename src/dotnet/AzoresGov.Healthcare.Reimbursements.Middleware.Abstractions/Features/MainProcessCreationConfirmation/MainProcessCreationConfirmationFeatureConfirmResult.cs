using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreationConfirmation
{
    public sealed class MainProcessCreationConfirmationFeatureConfirmResult
    {
        public MainProcessCreationConfirmationFeatureConfirmResult(Guid processId, Guid processRowVersionId)
        {
            ProcessId = processId;
            ProcessRowVersionId = processRowVersionId;
        }

        public Guid ProcessId { get; }

        public Guid ProcessRowVersionId { get; }
    }
}