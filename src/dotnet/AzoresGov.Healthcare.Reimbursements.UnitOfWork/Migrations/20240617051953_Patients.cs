using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

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
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PublicId = table.Column<Guid>(type: "char(36)", nullable: false),
                    RowVersionId = table.Column<Guid>(type: "char(36)", nullable: false),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    Number = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    TaxNumber = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Birth = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    Death = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
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
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.UniqueConstraint("AK_Patients_PublicId", x => x.PublicId);
                    table.ForeignKey(
                        name: "FK_Patients_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_EmailAddress",
                table: "Patients",
                column: "EmailAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_EntityId",
                table: "Patients",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_FaxNumber",
                table: "Patients",
                column: "FaxNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_MobileNumber",
                table: "Patients",
                column: "MobileNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Name",
                table: "Patients",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Number",
                table: "Patients",
                column: "Number",
                unique: true);

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
