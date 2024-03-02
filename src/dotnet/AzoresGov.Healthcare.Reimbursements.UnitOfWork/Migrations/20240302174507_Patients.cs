using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class Patients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Birth = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    HealthNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    TaxNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    AddressLine3 = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    PostalCodeArea = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FaxNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Death = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.UniqueConstraint("AK_Patients_PublicId", x => x.PublicId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_EmailAddress",
                table: "Patients",
                column: "EmailAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_HealthNumber",
                table: "Patients",
                column: "HealthNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_MobileNumber",
                table: "Patients",
                column: "MobileNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Name",
                table: "Patients",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PhoneNumber",
                table: "Patients",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_TaxNumber",
                table: "Patients",
                column: "TaxNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
