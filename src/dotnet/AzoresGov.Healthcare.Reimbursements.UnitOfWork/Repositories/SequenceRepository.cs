using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class SequenceRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, Sequence>, ISequenceRepository
    {
        public SequenceRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<Sequence?> GetByNameAsync(string name, CancellationToken ct) =>

            Entities.FirstOrDefaultAsync(e => e.Name == name, ct);
    }
}
