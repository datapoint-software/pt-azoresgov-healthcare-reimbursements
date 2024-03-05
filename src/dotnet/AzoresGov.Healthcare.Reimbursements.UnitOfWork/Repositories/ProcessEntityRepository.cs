using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class ProcessEntityRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, ProcessEntity>, IProcessEntityRepository
    {
        public ProcessEntityRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}