using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class InsertAdministrativeRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                "Roles",
                [ "PublicId", "RowVersionId", "Name" ],
                [ "ed686f97-135e-4ab1-af53-82d10817cee4", "2a081336-2cbc-4443-b775-51ce445db72f", "Administrative" ]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "Roles",
                "PublicId",
                "ed686f97-135e-4ab1-af53-82d10817cee4");
        }
    }
}
