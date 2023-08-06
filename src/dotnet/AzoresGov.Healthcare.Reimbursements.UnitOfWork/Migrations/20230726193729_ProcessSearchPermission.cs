using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable IDE0022

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class ProcessSearchPermission : Migration
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
                    "a5fd974b-976e-4a2f-92ab-28ad75c97738",
                    "5547fe3f-ec5d-48f1-92e2-6387df794d99",
                    "process-search"
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "Permissions",
                "PublicId",
                "a5fd974b-976e-4a2f-92ab-28ad75c97738");
        }
    }
}
