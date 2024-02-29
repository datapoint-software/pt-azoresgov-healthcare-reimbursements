using AzoresGov.Healthcare.Reimbursements.Enumerations;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class InsertAccessPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                "Permissions",
                [ "PublicId", "RowVersionId", "Name", "Granted" ],
                [ "55389c2b-807b-4311-9a23-a0d78a7ae383", "2f7cdac9-6683-4a2a-b12c-edfb5716c4fa", "Access", true ]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "Permissions",
                "Name",
                "Access");
        }
    }
}
