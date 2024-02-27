using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserSessionRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsContext, UserSession>, IUserSessionRepository
    {
        public UserSessionRepository(HealthcareReimbursementsContext context) : base(context)
        {
        }
    }
}
