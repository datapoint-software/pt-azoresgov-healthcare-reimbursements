namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCaptureBankResultModel
    {
        public ProcessCaptureBankResultModel(string name, string swiftCode)
        {
            Name = name;
            SwiftCode = swiftCode;
        }

        public string Name { get; }
        
        public string SwiftCode { get; }
    }
}