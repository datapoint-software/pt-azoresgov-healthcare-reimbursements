using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureBankQueryHandler : IQueryHandler<ProcessCaptureBankQuery, ProcessCaptureBankResult>
    {
        private readonly IBankRepository _banks;

        public ProcessCaptureBankQueryHandler(IBankRepository banks)
        {
            _banks = banks;
        }

        public async Task<ProcessCaptureBankResult> HandleQueryAsync(ProcessCaptureBankQuery query, CancellationToken ct)
        {
            var swiftLookupCode = query.Iban[ ..2 ] + query.Iban[ 4..8 ];

            var bank = await _banks.GetBySwiftLookupCodeAsync(
                swiftLookupCode,
                ct);
            
            Assert.Found(bank);

            return new ProcessCaptureBankResult(
                bank.Name,
                bank.SwiftCode);
        }
    }
}