using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.UnitOfWork;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork
{
    public interface IHealthcareReimbursementsUnitOfWork : IUnitOfWork
    {
        IParameterRepository Parameters { get; }

        IUserPasswordRepository UserPasswords { get; }

        IUserRepository Users { get; }

        IUserSessionRepository UserSessions { get; }
    }
}
