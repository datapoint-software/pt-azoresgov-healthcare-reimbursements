using Datapoint.Mediator;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureBankQuery : Query<ProcessCaptureBankResult>
    {
        public ProcessCaptureBankQuery(string iban)
        {
            Iban = iban;
        }

        public string Iban { get; }
    }
}