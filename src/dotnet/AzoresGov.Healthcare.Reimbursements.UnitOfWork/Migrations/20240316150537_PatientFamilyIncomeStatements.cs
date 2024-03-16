using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class PatientFamilyIncomeStatements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientFamilyIncomeStatements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    AccessCode = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    FamilyMemberCount = table.Column<int>(type: "int", nullable: false),
                    FamilyIncome = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientFamilyIncomeStatements", x => x.Id);
                    table.UniqueConstraint("AK_PatientFamilyIncomeStatements_PublicId", x => x.PublicId);
                    table.ForeignKey(
                        name: "FK_PatientFamilyIncomeStatements_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientFamilyIncomeStatements_PatientId",
                table: "PatientFamilyIncomeStatements",
                column: "PatientId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientFamilyIncomeStatements");
        }
    }
}
