using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class ProcessPaymentSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessPaymentSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessId = table.Column<long>(type: "bigint", nullable: false),
                    Method = table.Column<int>(type: "int", nullable: false),
                    Receiver = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessPaymentSettings", x => x.Id);
                    table.UniqueConstraint("AK_ProcessPaymentSettings_PublicId", x => x.PublicId);
                    table.ForeignKey(
                        name: "FK_ProcessPaymentSettings_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessPaymentSettings_ProcessId",
                table: "ProcessPaymentSettings",
                column: "ProcessId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessPaymentSettings");
        }
    }
}
