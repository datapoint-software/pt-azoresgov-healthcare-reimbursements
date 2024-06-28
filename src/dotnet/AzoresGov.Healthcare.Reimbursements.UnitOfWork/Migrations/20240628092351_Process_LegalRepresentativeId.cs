using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class Process_LegalRepresentativeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LegalRepresentativeId",
                table: "Processes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Processes_LegalRepresentativeId",
                table: "Processes",
                column: "LegalRepresentativeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_LegalRepresentatives_LegalRepresentativeId",
                table: "Processes",
                column: "LegalRepresentativeId",
                principalTable: "LegalRepresentatives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql("ALTER TABLE Processes MODIFY LegalRepresentativeId BIGINT NULL AFTER PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_LegalRepresentatives_LegalRepresentativeId",
                table: "Processes");

            migrationBuilder.DropIndex(
                name: "IX_Processes_LegalRepresentativeId",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "LegalRepresentativeId",
                table: "Processes");
        }
    }
}
