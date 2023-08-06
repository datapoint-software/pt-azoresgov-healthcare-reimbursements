using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable IDE0022

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class ProcessCreationPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                "Permissions",
                new[]
                {
                    "PublicId",
                    "RowVersionId",
                    "Name"
                },
                new[]
                {
                    "88248a63-bd0c-4c85-9bd9-ee2b8b60a0eb",
                    "72f668f9-7324-4b2b-a94b-8e067ff34192",
                    "process-creation"
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "Permissions",
                "PublicId",
                "88248a63-bd0c-4c85-9bd9-ee2b8b60a0eb");
        }
    }
}
