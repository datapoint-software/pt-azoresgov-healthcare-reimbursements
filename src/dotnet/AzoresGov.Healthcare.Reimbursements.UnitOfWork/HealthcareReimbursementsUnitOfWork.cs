using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.UnitOfWork.EntityFrameworkCore;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork
{
    public sealed class HealthcareReimbursementsUnitOfWork : EntityFrameworkCoreUnitOfWork<HealthcareReimbursementsContext>, IHealthcareReimbursementsUnitOfWork
    {
        private ParameterRepository? _parameters;

        private UserPasswordRepository? _userPasswords;

        private UserRepository? _users;

        private UserSessionRepository? _userSessions;

        public HealthcareReimbursementsUnitOfWork(HealthcareReimbursementsContext context) : base(context)
        {
        }

        public IParameterRepository Parameters => _parameters 
            ??= new ParameterRepository(Context);

        public IUserPasswordRepository UserPasswords => _userPasswords
            ??= new UserPasswordRepository(Context);

        public IUserRepository Users => _users 
            ??= new UserRepository(Context);

        public IUserSessionRepository UserSessions => _userSessions
            ??= new UserSessionRepository(Context);
    }
}
