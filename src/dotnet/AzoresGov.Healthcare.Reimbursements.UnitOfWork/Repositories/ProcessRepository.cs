using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class ProcessRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, Process>, IProcessRepository
    {
        public ProcessRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
