using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserSessionRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, UserSession>, IUserSessionRepository
    {
        public UserSessionRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
