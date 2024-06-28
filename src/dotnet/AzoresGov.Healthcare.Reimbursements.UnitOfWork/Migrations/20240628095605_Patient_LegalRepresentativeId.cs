using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class Patient_LegalRepresentativeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LegalRepresentativeId",
                table: "Patients",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_LegalRepresentativeId",
                table: "Patients",
                column: "LegalRepresentativeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_LegalRepresentatives_LegalRepresentativeId",
                table: "Patients",
                column: "LegalRepresentativeId",
                principalTable: "LegalRepresentatives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);


            migrationBuilder.Sql("ALTER TABLE Patients MODIFY LegalRepresentativeId BIGINT NULL AFTER EntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_LegalRepresentatives_LegalRepresentativeId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_LegalRepresentativeId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "LegalRepresentativeId",
                table: "Patients");
        }
    }
}
