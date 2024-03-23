namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureSimulationLineResult
    {
        public ProcessCaptureSimulationLineResult(string description, decimal? amount)
        {
            Description = description;
            Amount = amount;
        }

        public string Description { get; }

        public decimal? Amount { get; }
    }
}