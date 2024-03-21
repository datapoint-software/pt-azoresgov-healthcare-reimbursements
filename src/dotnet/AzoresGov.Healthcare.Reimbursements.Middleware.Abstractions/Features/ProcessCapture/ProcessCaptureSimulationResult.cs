using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureSimulationResult
    {
        public ProcessCaptureSimulationResult(IReadOnlyCollection<ProcessCaptureSimulationLineResult> lines, decimal multiplier)
        {
            Lines = lines;
            Multiplier = multiplier;
        }

        public IReadOnlyCollection<ProcessCaptureSimulationLineResult> Lines { get; }

        public decimal Multiplier { get; }
    }
}