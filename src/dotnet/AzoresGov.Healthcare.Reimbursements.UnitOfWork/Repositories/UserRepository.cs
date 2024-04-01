using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, User>, IUserRepository
    {
        public UserRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
