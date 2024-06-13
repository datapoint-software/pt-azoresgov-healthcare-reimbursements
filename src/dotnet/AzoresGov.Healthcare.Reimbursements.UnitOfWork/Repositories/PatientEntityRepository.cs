using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class PatientEntityRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, PatientEntity>, IPatientEntityRepository
    {
        public PatientEntityRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IReadOnlyCollection<PatientEntity>> GetAllByPatientIdAndEntityIdAsync(IReadOnlyCollection<long> patientId, IReadOnlyCollection<long> entityId, CancellationToken ct)
        {
            return await Entities
                .Where(e => patientId.Contains(e.PatientId) && entityId.Contains(e.EntityId))
                .ToListAsync(ct);
        }
    }
}
