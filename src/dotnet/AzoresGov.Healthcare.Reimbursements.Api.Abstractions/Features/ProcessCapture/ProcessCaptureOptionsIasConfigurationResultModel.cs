namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCaptureOptionsIasConfigurationResultModel
    {
        public ProcessCaptureOptionsIasConfigurationResultModel(int year, decimal amount)
        {
            Year = year;
            Amount = amount;
        }

        public int Year { get; }

        public decimal Amount { get; }
    }
}