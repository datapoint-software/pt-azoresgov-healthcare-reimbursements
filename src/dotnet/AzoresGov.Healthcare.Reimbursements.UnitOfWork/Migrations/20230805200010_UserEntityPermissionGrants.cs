using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class UserEntityPermissionGrants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
CREATE VIEW UserEntityPermissionGrants AS (
	SELECT
		u.Id AS UserId,
		e.Id AS EntityId,
		p.Id AS PermissionId,
		ISNULL((
			SELECT TOP 1 Granted
			FROM UserPermissions up
			WHERE up.UserId = u.Id AND up.PermissionId = p.Id
		), ISNULL((
			SELECT CAST(MIN(CAST(rp.Granted AS INT)) AS BIT)
			FROM UserRoles ur 
			INNER JOIN RolePermissions rp ON rp.RoleId = ur.RoleId
			WHERE ur.UserId = u.Id
		), ISNULL((
			SELECT CAST(MIN(CAST(uep.Granted AS INT)) AS BIT)
			FROM UserEntityPermissions uep
			INNER JOIN UserEntities ue ON 
				ue.UserId = uep.UserId AND 
				ue.EntityId = uep.EntityId
			WHERE uep.UserId = u.Id AND
				uep.EntityId = e.Id AND
				uep.PermissionId = p.Id
		), ISNULL((
			SELECT CAST(MIN(CAST(rp.Granted AS INT)) AS BIT)
			FROM UserEntityRoles uer
			INNER JOIN UserEntities ue ON
				ue.UserId = uer.UserId AND
				ue.EntityId = uer.EntityId
			INNER JOIN RolePermissions rp ON
				rp.RoleId = uer.RoleId 
			WHERE uer.UserId = u.Id AND
				uer.EntityId = e.Id AND
				rp.PermissionId = p.Id
		), (
			0
		))))) Granted
	FROM Users u, Entities e, Permissions p
)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP VIEW UserEntityPermissionGrants");
        }
    }
}
