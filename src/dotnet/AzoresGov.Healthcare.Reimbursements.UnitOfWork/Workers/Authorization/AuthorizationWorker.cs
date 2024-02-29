using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Dapper;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Workers.Authorization
{
    public sealed class AuthorizationWorker : EntityFrameworkCoreWorker<HealthcareReimbursementsUnitOfWork>, IAuthorizationWorker
    {
        public AuthorizationWorker(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<bool> IsUserEntityPermissionGrantedAsync(long userId, long permissionId, CancellationToken ct) =>
            
            await UnitOfWork.Database
                .GetDbConnection()
                .ExecuteScalarAsync<bool>(
                    @"
SELECT 
	MAX(
		ISNULL(x.UserPermissionGrant,
			ISNULL(x.RolePermissionGrant,
				ISNULL(x.UserEntityPermissionGrant,
					ISNULL(x.UserEntityRolePermissionGrant,
						x.PermissionGrant))))) Granted
FROM
(
	SELECT 
		u.Id UserId,
		e.Id EntityId,
		p.Id PermissionId,
		p.Granted PermissionGrant,
		(
			SELECT MIN(CAST(up.Granted AS INT))
			FROM UserPermissions up
			WHERE up.UserId = u.Id AND up.PermissionId = p.Id
		) UserPermissionGrant,
		(
			SELECT MIN(CAST(rp.Granted AS INT))
			FROM UserRoles ur
			INNER JOIN RolePermissions rp ON rp.RoleId = ur.RoleId
			WHERE ur.UserId = u.Id AND rp.PermissionId = p.Id
		) RolePermissionGrant,
		(
			SELECT MIN(CAST(uep.Granted AS INT))
			FROM UserEntities ue 
			INNER JOIN UserEntityPermissions uep ON  uep.UserId = ue.UserId AND  uep.EntityId = ue.EntityId
			WHERE ue.UserId = u.Id AND ue.EntityId = e.Id AND uep.PermissionId = p.Id
		) UserEntityPermissionGrant,
		(
			SELECT MIN(CAST(rp.Granted AS INT))
			FROM UserEntities ue
			INNER JOIN UserEntityRoles uer ON uer.UserId = ue.UserId AND uer.EntityId = ue.EntityId
			INNER JOIN Roles r ON r.Id = uer.RoleId 
			INNER JOIN RolePermissions rp ON rp.RoleId = r.Id
			WHERE ue.UserId = u.Id AND ue.EntityId = e.Id AND rp.PermissionId = p.Id
		) UserEntityRolePermissionGrant
	FROM Users u, Permissions p, Entities e
	WHERE u.Id = :UserId AND p.Id = :PermissionId
) x",
                    new
                    {
                        UserId = userId, 
                        PermissionId = permissionId
                    });

        public async Task<bool> IsUserEntityPermissionGrantedAsync(long userId, long entityId, long permissionId, CancellationToken ct) =>
            
            await UnitOfWork.Database
                .GetDbConnection()
                .ExecuteScalarAsync<bool>(
                    @"
SELECT 
	ISNULL(x.UserPermissionGrant,
		ISNULL(x.RolePermissionGrant,
			ISNULL(x.UserEntityPermissionGrant,
				ISNULL(x.UserEntityRolePermissionGrant,
					x.PermissionGrant)))) Granted
FROM
(
	SELECT 
		u.Id UserId,
		e.Id EntityId,
		p.Id PermissionId,
		p.Granted PermissionGrant,
		(
			SELECT MIN(CAST(up.Granted AS INT))
			FROM UserPermissions up
			WHERE up.UserId = u.Id AND up.PermissionId = p.Id
		) UserPermissionGrant,
		(
			SELECT MIN(CAST(rp.Granted AS INT))
			FROM UserRoles ur
			INNER JOIN RolePermissions rp ON rp.RoleId = ur.RoleId
			WHERE ur.UserId = u.Id AND rp.PermissionId = p.Id
		) RolePermissionGrant,
		(
			SELECT MIN(CAST(uep.Granted AS INT))
			FROM UserEntities ue 
			INNER JOIN UserEntityPermissions uep ON  uep.UserId = ue.UserId AND  uep.EntityId = ue.EntityId
			WHERE ue.UserId = u.Id AND ue.EntityId = e.Id AND uep.PermissionId = p.Id
		) UserEntityPermissionGrant,
		(
			SELECT MIN(CAST(rp.Granted AS INT))
			FROM UserEntities ue
			INNER JOIN UserEntityRoles uer ON uer.UserId = ue.UserId AND uer.EntityId = ue.EntityId
			INNER JOIN Roles r ON r.Id = uer.RoleId 
			INNER JOIN RolePermissions rp ON rp.RoleId = r.Id
			WHERE ue.UserId = u.Id AND ue.EntityId = e.Id AND rp.PermissionId = p.Id
		) UserEntityRolePermissionGrant
	FROM Users u, Permissions p, Entities e
	WHERE u.Id = :UserId AND p.Id = :PermissionId AND e.Id = :EntityId
) x",
                    new
                    {
                        UserId = userId, 
                        EntityId = entityId, 
                        PermissionId = permissionId
                    });

        public async Task<bool> IsUserPermissionGrantedAsync(long userId, long permissionId, CancellationToken ct) => 
            
            await UnitOfWork.Database
                .GetDbConnection()
                .ExecuteScalarAsync<bool>(
                    @"
SELECT 
	ISNULL(x.UserPermissionGrant,
		ISNULL(x.RolePermissionGrant,
			x.PermissionGrant)) Granted
FROM
(
	SELECT 
		u.Id UserId,
		p.Id PermissionId,
		p.Granted PermissionGrant,
		(
			SELECT MIN(CAST(up.Granted AS INT))
			FROM UserPermissions up
			WHERE up.UserId = u.Id AND up.PermissionId = p.Id
		) UserPermissionGrant,
		(
			SELECT MIN(CAST(rp.Granted AS INT))
			FROM UserRoles ur
			INNER JOIN RolePermissions rp ON rp.RoleId = ur.RoleId
			WHERE ur.UserId = u.Id AND rp.PermissionId = p.Id
		) RolePermissionGrant
	FROM Users u, Permissions p
	WHERE u.Id = :UserId AND p.Id = :PermissionId
) x",
                    new
                    {
                        UserId = userId, 
                        PermissionId = permissionId
                    });
    }
}