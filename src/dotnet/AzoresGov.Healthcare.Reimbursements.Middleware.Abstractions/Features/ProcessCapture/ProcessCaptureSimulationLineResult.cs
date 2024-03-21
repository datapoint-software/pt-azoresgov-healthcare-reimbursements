namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureSimulationLineResult
    {
        public ProcessCaptureSimulationLineResult(string description, string? value)
        {
            Description = description;
            Value = value;
        }

        public string Description { get; }

        public string? Value { get; }
    }
}