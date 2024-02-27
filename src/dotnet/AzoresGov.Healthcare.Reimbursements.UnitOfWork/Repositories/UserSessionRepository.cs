using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserSessionRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsContext, UserSessionEntity>, IUserSessionRepository
    {
        public UserSessionRepository(HealthcareReimbursementsContext context) : base(context)
        {
        }
    }
}
