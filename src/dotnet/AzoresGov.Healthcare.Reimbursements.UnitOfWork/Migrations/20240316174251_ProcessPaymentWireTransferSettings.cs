using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class ProcessPaymentWireTransferSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessPaymentWireTransferSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessId = table.Column<long>(type: "bigint", nullable: false),
                    Iban = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Swift = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessPaymentWireTransferSettings", x => x.Id);
                    table.UniqueConstraint("AK_ProcessPaymentWireTransferSettings_PublicId", x => x.PublicId);
                    table.ForeignKey(
                        name: "FK_ProcessPaymentWireTransferSettings_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessPaymentWireTransferSettings_ProcessId",
                table: "ProcessPaymentWireTransferSettings",
                column: "ProcessId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessPaymentWireTransferSettings");
        }
    }
}
