using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IPatientEntityRepository : IRepository<PatientEntity>
    {
        Task<IReadOnlyCollection<PatientEntity>> GetAllByPatientIdAndEntityIdAsync(
            IReadOnlyCollection<long> patientId, 
            IReadOnlyCollection<long> entityId, 
            CancellationToken ct);
    }
}
