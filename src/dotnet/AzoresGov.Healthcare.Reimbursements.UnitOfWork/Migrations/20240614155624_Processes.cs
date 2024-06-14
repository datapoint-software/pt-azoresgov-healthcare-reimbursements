using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

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
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PublicId = table.Column<Guid>(type: "char(36)", nullable: false),
                    RowVersionId = table.Column<Guid>(type: "char(36)", nullable: false),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    Number = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Creation = table.Column<DateTimeOffset>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processes", x => x.Id);
                    table.UniqueConstraint("AK_Processes_Number", x => x.Number);
                    table.UniqueConstraint("AK_Processes_PublicId", x => x.PublicId);
                    table.ForeignKey(
                        name: "FK_Processes_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Processes_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_EntityId",
                table: "Processes",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_PatientId",
                table: "Processes",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Processes");
        }
    }
}
