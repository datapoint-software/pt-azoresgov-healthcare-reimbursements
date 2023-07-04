using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.UnitOfWork;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork
{
    public interface IHealthcareReimbursementsUnitOfWork : IUnitOfWork
    {
        IEntityRepository Entities { get; }

        IParameterRepository Parameters { get; }

        IPermissionRepository Permissions { get; }

        IRolePermissionRepository RolePermissions { get; }

        IRoleRepository Roles { get; }

        IUserAgentRepository UserAgents { get; }

        IUserEmailAddressRepository UserEmailAddresses { get; }

        IUserEntityRepository UserEntities { get; }

        IUserEntityPermissionRepository UserEntityPermissions { get; }

        IUserEntityRoleRepository UserEntityRoles { get; }

        IUserPasswordRepository UserPasswords { get; }

        IUserPermissionRepository UserPermissions { get; }

        IUserRoleRepository UserRoles { get; }

        IUserRepository Users { get; }

        IUserSessionRepository UserSessions { get; }
    }
}
