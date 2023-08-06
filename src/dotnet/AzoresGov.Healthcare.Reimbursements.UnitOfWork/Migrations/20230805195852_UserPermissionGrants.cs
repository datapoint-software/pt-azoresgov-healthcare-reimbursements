using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class UserPermissionGrants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE VIEW UserPermissionGrants AS (
	SELECT 
		u.Id AS UserId,
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
		), (
			0
		))) Granted
	FROM Users u, Permissions p
)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW UserPermissionGrants");
        }
    }
}
