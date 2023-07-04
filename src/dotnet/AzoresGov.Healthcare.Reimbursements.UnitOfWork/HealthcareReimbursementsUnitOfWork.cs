using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.UnitOfWork.EntityFrameworkCore;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork
{
    public sealed class HealthcareReimbursementsUnitOfWork : EntityFrameworkCoreUnitOfWork<HealthcareReimbursementsContext>, IHealthcareReimbursementsUnitOfWork
    {
        private EntityRepository? _entities;

        private ParameterRepository? _parameters;

        private PermissionRepository? _permissions;

        private RolePermissionRepository? _rolePermissions;

        private RoleRepository? _roles;

        private UserAgentRepository? _userAgents;

        private UserEmailAddressRepository? _userEmailAddresses;

        private UserEntityRepository? _userEntities;

        private UserEntityPermissionRepository? _userEntityPermissions;

        private UserEntityRoleRepository? _userEntityRoles;

        private UserPasswordRepository? _userPasswords;

        private UserPermissionRepository? _userPermissions;

        private UserRoleRepository? _userRoles;

        private UserRepository? _users;

        private UserSessionRepository? _userSessions;

        public HealthcareReimbursementsUnitOfWork(HealthcareReimbursementsContext context) : base(context)
        {
        }

        public IEntityRepository Entities => _entities ??= new EntityRepository(this);

        public IParameterRepository Parameters => _parameters ??= new ParameterRepository(this);

        public IPermissionRepository Permissions => _permissions ??= new PermissionRepository(this);

        public IRolePermissionRepository RolePermissions => _rolePermissions ??= new RolePermissionRepository(this);

        public IRoleRepository Roles => _roles ??= new RoleRepository(this);

        public IUserAgentRepository UserAgents => _userAgents ??= new UserAgentRepository(this);

        public IUserEmailAddressRepository UserEmailAddresses => _userEmailAddresses ??= new UserEmailAddressRepository(this);

        public IUserEntityRepository UserEntities => _userEntities ??= new UserEntityRepository(this);

        public IUserEntityPermissionRepository UserEntityPermissions => _userEntityPermissions ??= new UserEntityPermissionRepository(this);

        public IUserEntityRoleRepository UserEntityRoles => _userEntityRoles ??= new UserEntityRoleRepository(this);

        public IUserPasswordRepository UserPasswords => _userPasswords ??= new UserPasswordRepository(this);

        public IUserPermissionRepository UserPermissions => _userPermissions ??= new UserPermissionRepository(this);

        public IUserRoleRepository UserRoles => _userRoles ??= new UserRoleRepository(this);

        public IUserRepository Users => _users ??= new UserRepository(this);

        public IUserSessionRepository UserSessions => _userSessions ??= new UserSessionRepository(this);
    }
}
