using AzoresGov.Healthcare.Reimbursements.Enumerations;
using Dapper;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Workers.Features.ProcessCreation
{
    public sealed class ProcessCreationEntitySearchWorker : EntityFrameworkCoreWorker<HealthcareReimbursementsUnitOfWork>, IProcessCreationEntitySearchWorker
    {
        public ProcessCreationEntitySearchWorker(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IReadOnlyCollection<ProcessCreationEntitySearchResult>> SearchUserEntitiesWithPermissionGrantAsync(long userId, long permissionId, IReadOnlyCollection<EntityNature> nature, string? filter, int skip, int take, CancellationToken ct)
        {
            string @filterExpression;

            if (string.IsNullOrEmpty(filter))
            {
                filterExpression = string.Empty;
            }
            else
            {
                filter = $"%{filter.Replace(' ', '%')}%";
                filterExpression = "(e.Name LIKE @Filter OR e.Code LIKE @Filter) AND";
            }
            
            var results = await UnitOfWork.Database
                .GetDbConnection()
                .QueryAsync<ProcessCreationEntitySearchResult>(@"
SELECT 
	e.Id,
	e.PublicId,
	e.RowVersionId,
	epe.ParentEntityId,
	e.Code,
	e.Name,
	e.Nature
FROM UserEntities ue
INNER JOIN Entities e ON e.Id = ue.EntityId AND e.Nature IN @EntityNature
INNER JOIN EntityParentEntities epe ON epe.EntityId = e.Id AND epe.Level = 0
WHERE ue.UserId = @UserId AND" + filterExpression + @"
(
	EXISTS (
		SELECT *
		FROM UserPermissions up
		WHERE up.UserId = ue.UserId AND up.PermissionId = @PermissionId AND up.Granted = 1
	)

	OR EXISTS (
		SELECT *
		FROM UserRoles ur
		INNER JOIN RolePermissions rp ON rp.RoleId = ur.RoleId AND rp.PermissionId = @PermissionId AND rp.Granted = 1
		WHERE ur.UserId = ue.UserId
	)
	
	OR EXISTS (
		SELECT *
		FROM UserEntityPermissions uep
		WHERE uep.UserId = ue.UserId AND uep.EntityId = ue.EntityId AND uep.PermissionId = @PermissionId AND uep.Granted = 1
	)

	OR EXISTS (
		SELECT *
		FROM UserEntityRoles uer 
		INNER JOIN RolePermissions rp ON rp.RoleId = uer.RoleId
		WHERE uer.UserId = ue.UserId AND uer.EntityId = ue.EntityId AND rp.PermissionId = @PermissionId AND rp.Granted = 1
	)

	OR EXISTS (
		SELECT *
		FROM UserEntityRoles uer 
		INNER JOIN RolePermissions rp ON rp.RoleId = uer.RoleId
		WHERE uer.UserId = ue.UserId AND uer.EntityId = ue.EntityId AND rp.PermissionId = @PermissionId AND rp.Granted = 1
	)
)
ORDER BY e.Name, e.Code ASC
OFFSET (@Skip) ROWS FETCH NEXT (@Take) ROWS ONLY",
                    new
                    {
                        UserId = userId,
                        PermissionId = permissionId,
                        EntityNature = nature,
                        Filter = filter,
                        Skip = skip,
                        Take = take
                    });

            return results.ToArray();
        }
    }
}