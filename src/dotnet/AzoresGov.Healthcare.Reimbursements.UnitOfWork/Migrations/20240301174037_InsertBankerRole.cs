using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class InsertBankerRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                "Roles",
                [ "PublicId", "RowVersionId", "Name" ],
                [ "9ebdf1b9-282c-42e7-b9c6-4c427770cf9e", "1e036bb8-4be1-448c-9b3a-5f19506b9d3d", "Banker" ]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "Roles",
                "PublicId",
                "9ebdf1b9-282c-42e7-b9c6-4c427770cf9e");
        }
    }
}
