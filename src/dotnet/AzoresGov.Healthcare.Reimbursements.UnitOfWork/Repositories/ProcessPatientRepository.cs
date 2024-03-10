using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class ProcessPatientRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, ProcessPatient>, IProcessPatientRepository
    {
        public ProcessPatientRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        
        public async Task<IReadOnlyCollection<ProcessPatient>> GetAllByProcessIdAsync(IReadOnlyCollection<long> processId, CancellationToken ct) =>

            await Entities
                .Where(e => processId.Contains(e.ProcessId))
                .ToListAsync(ct);

        public Task<ProcessPatient?> GetByProcessIdAsync(
            long processId,
            CancellationToken ct) =>

            Entities.FirstOrDefaultAsync(e => e.ProcessId == processId, ct);
    }
}