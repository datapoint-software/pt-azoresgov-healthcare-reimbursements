using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserPasswordRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, UserPassword>, IUserPasswordRepository
    {
        public UserPasswordRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<UserPassword?> GetByUserIdAsync(long userId, CancellationToken ct) =>

            Entities.FirstOrDefaultAsync(e => e.UserId == userId, ct);
    }
}
