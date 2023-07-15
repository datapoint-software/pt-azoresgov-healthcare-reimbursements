using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class PermissionRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, HealthcareReimbursementsContext, PermissionEntity>, IPermissionRepository
    {
        public PermissionRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<PermissionEntity>> GetAllByIdAsync(IEnumerable<long> id, CancellationToken ct) => await Entities

            .Where(e => id.Contains(e.Id))
            .ToListAsync(ct);
    }
}
