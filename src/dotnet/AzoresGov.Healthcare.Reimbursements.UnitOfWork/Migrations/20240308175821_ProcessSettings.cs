using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class ProcessSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessId = table.Column<long>(type: "bigint", nullable: false),
                    MachadoJosephEnabled = table.Column<bool>(type: "bit", nullable: false),
                    DocumentIssueDateBypassEnabled = table.Column<bool>(type: "bit", nullable: false),
                    ReimbursementLimitBypassEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessSettings", x => x.Id);
                    table.UniqueConstraint("AK_ProcessSettings_PublicId", x => x.PublicId);
                    table.ForeignKey(
                        name: "FK_ProcessSettings_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessSettings_ProcessId",
                table: "ProcessSettings",
                column: "ProcessId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessSettings");
        }
    }
}
