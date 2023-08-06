using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

#pragma warning disable IDE0022

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class UserAgents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAgents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Signature = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAgents", x => x.Id);
                    table.UniqueConstraint("AK_UserAgents_PublicId", x => x.PublicId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAgents_Hash",
                table: "UserAgents",
                column: "Hash",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAgents");
        }
    }
}
