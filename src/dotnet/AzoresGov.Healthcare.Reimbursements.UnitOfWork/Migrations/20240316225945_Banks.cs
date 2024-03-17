using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class Banks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SwiftCode = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    SwiftLookupCode = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                    table.UniqueConstraint("AK_Banks_PublicId", x => x.PublicId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banks_SwiftCode",
                table: "Banks",
                column: "SwiftCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Banks_SwiftLookupCode",
                table: "Banks",
                column: "SwiftLookupCode",
                unique: true,
                filter: "[SwiftLookupCode] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banks");
        }
    }
}
