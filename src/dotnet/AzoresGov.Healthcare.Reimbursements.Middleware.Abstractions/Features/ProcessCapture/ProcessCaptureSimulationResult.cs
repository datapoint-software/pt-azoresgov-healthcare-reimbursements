using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureSimulationResult
    {
        public ProcessCaptureSimulationResult(IReadOnlyCollection<ProcessCaptureSimulationLineResult> lines, decimal percentage)
        {
            Lines = lines;
            Percentage = percentage;
        }

        public IReadOnlyCollection<ProcessCaptureSimulationLineResult> Lines { get; }

        public decimal Percentage { get; set; }
    }
}