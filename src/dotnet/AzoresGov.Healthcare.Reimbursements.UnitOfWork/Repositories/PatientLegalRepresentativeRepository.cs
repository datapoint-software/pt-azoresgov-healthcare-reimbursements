using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class PatientLegalRepresentativeRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, PatientLegalRepresentative>, IPatientLegalRepresentativeRepository
    {
        public PatientLegalRepresentativeRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<PatientLegalRepresentative?> GetByPatientIdAsync(long patientId, CancellationToken ct) =>

            Entities.FirstOrDefaultAsync(e => e.PatientId == patientId, ct);
    }
}