using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class PatientEntityRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, PatientEntity>, IPatientEntityRepository
    {
        public PatientEntityRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<bool> AnyByPatientIdAndEntityIdAsync(long patientId, long entityId, CancellationToken ct) =>

            Entities.AnyAsync(pe => pe.PatientId == patientId && pe.EntityId == entityId, ct);
    }
}