using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class ProcessConfigurationRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, ProcessConfiguration>, IProcessConfigurationRepository
    {
        public ProcessConfigurationRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<ProcessConfiguration?> GetByProcessIdAsync(long processId, CancellationToken ct) =>

            Entities.FirstOrDefaultAsync(e => e.ProcessId == processId, ct);
    }
}