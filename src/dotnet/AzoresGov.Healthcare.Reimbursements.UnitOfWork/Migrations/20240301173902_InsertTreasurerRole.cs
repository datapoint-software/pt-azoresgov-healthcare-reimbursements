using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class InsertTreasurerRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                "Roles",
                [ "PublicId", "RowVersionId", "Name" ],
                [ "10f9d055-9588-4a5b-aee7-aaf9cec9c245", "308ccbc4-09a1-4cde-879f-b59906c80e44", "Treasurer" ]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "Roles",
                "PublicId",
                "10f9d055-9588-4a5b-aee7-aaf9cec9c245");
        }
    }
}
