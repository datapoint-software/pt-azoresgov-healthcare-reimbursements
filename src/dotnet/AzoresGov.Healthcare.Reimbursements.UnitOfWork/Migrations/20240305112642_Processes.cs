using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class Processes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Processes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Creation = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Expiration = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Touch = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processes", x => x.Id);
                    table.UniqueConstraint("AK_Processes_PublicId", x => x.PublicId);
                    table.ForeignKey(
                        name: "FK_Processes_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Processes_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Processes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Processes_Creation",
                table: "Processes",
                column: "Creation");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_EntityId",
                table: "Processes",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_Expiration",
                table: "Processes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_Number",
                table: "Processes",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Processes_PatientId",
                table: "Processes",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_Touch",
                table: "Processes",
                column: "Touch");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_UserId",
                table: "Processes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Processes");
        }
    }
}
