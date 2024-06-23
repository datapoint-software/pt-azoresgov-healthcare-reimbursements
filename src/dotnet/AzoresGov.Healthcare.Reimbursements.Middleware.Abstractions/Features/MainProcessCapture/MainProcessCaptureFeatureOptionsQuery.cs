using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureOptionsQuery : Query<MainProcessCaptureFeatureOptionsResult>
    {
        public MainProcessCaptureFeatureOptionsQuery(Guid userId, Guid processId)
        {
            UserId = userId;
            ProcessId = processId;
        }

        public Guid UserId { get; }

        public Guid ProcessId { get; }
    }
}
