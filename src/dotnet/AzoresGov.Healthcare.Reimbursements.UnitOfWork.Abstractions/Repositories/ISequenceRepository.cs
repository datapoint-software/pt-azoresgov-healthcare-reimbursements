using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface ISequenceRepository : IRepository<Sequence>
    {
        Task<Sequence?> GetByNameAsync(string name, CancellationToken ct);
    }
}
