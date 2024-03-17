namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureBankResult
    {
        public ProcessCaptureBankResult(string name, string swiftCode)
        {
            Name = name;
            SwiftCode = swiftCode;
        }

        public string Name { get; }
        
        public string SwiftCode { get; }
    }
}