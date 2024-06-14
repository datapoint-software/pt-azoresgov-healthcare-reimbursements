using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Management
{
    public interface IEntityManager
    {
        Task<string> GetProcessNumberSequenceNameAsync(Entity entity, int processYear, CancellationToken ct);
    }
}
