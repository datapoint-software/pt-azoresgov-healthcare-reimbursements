using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserPasswordRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, UserPassword>, IUserPasswordRepository
    {
        public UserPasswordRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
