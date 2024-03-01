using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class InsertSupportRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                "Roles",
                [ "PublicId", "RowVersionId", "Name" ],
                [ "084a3276-c1f6-4ce9-b230-21a6bc973491", "2a32ef57-7c6c-4023-b383-e48da3e6a3ff", "Support" ]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "Roles",
                "PublicId",
                "084a3276-c1f6-4ce9-b230-21a6bc973491");
        }
    }
}
