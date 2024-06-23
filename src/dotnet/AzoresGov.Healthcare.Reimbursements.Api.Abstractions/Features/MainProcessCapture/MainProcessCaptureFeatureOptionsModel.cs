using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureOptionsModel
    {
        public MainProcessCaptureFeatureOptionsModel(Guid processId)
        {
            ProcessId = processId;
        }

        public Guid ProcessId { get; }
    }
}
