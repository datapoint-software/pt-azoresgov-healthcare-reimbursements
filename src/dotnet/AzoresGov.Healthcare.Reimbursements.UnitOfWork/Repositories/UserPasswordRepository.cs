using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserPasswordRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, UserPassword>, IUserPasswordRepository
    {
        public UserPasswordRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<UserPassword?> GetByUserIdAsync(long userId, CancellationToken ct) =>

            Entities.FirstOrDefaultAsync(e => e.User.Id == userId, ct);
    }
}
