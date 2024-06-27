using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class LegalRepresentatives : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LegalRepresentatives",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PublicId = table.Column<Guid>(type: "char(36)", nullable: false),
                    RowVersionId = table.Column<Guid>(type: "char(36)", nullable: false),
                    TaxNumber = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    FaxNumber = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true),
                    MobileNumber = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true),
                    EmailAddress = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    PostalAddressArea = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    PostalAddressAreaCode = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    PostalAddressLine1 = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    PostalAddressLine2 = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    PostalAddressLine3 = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalRepresentatives", x => x.Id);
                    table.UniqueConstraint("AK_LegalRepresentatives_PublicId", x => x.PublicId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LegalRepresentatives_EmailAddress",
                table: "LegalRepresentatives",
                column: "EmailAddress");

            migrationBuilder.CreateIndex(
                name: "IX_LegalRepresentatives_FaxNumber",
                table: "LegalRepresentatives",
                column: "FaxNumber");

            migrationBuilder.CreateIndex(
                name: "IX_LegalRepresentatives_MobileNumber",
                table: "LegalRepresentatives",
                column: "MobileNumber");

            migrationBuilder.CreateIndex(
                name: "IX_LegalRepresentatives_Name",
                table: "LegalRepresentatives",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_LegalRepresentatives_PhoneNumber",
                table: "LegalRepresentatives",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_LegalRepresentatives_TaxNumber",
                table: "LegalRepresentatives",
                column: "TaxNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LegalRepresentatives");
        }
    }
}
