namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureOptionsIasConfigurationResult
    {
        public ProcessCaptureOptionsIasConfigurationResult(int year, decimal amount)
        {
            Year = year;
            Amount = amount;
        }

        public int Year { get; }

        public decimal Amount { get; }
    }
}