using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class InsertValidatorRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                "Roles",
                [ "PublicId", "RowVersionId", "Name" ],
                [ "17db72b6-321b-45fd-979a-8340bc3fc563", "ac8e02ea-3966-4cb3-adc1-bcef1d6113d2", "Validator" ]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "Roles",
                "PublicId",
                "17db72b6-321b-45fd-979a-8340bc3fc563");
        }
    }
}
