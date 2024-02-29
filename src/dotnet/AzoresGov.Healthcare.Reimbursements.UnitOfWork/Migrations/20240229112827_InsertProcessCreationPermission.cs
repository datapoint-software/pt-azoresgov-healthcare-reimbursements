using AzoresGov.Healthcare.Reimbursements.Enumerations;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class InsertProcessCreationPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                "Permissions",
                [ "PublicId", "RowVersionId", "Name", "Granted" ],
                [ "1d20e2d8-f067-4a25-a62a-278ef7d16950", "7cc0d4b6-fdfc-44d5-a1a3-28d306645605", "ProcessCreation", false ]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "Permissions",
                "Name",
                "ProcessCreation");
        }
    }
}
