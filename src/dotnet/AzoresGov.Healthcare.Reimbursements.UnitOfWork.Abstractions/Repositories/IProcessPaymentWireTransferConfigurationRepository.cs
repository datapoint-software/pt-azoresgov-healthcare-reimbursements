using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IProcessPaymentWireTransferConfigurationRepository : IRepository<ProcessPaymentWireTransferConfiguration>
    {
        Task<ProcessPaymentWireTransferConfiguration?> GetByProcessIdAsync(
            long processId,
            CancellationToken ct);
    }
}