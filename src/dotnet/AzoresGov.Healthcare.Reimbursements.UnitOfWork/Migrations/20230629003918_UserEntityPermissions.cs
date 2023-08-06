using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

#pragma warning disable IDE0022

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class UserEntityPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserEntityPermissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false),
                    Granted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntityPermissions", x => x.Id);
                    table.UniqueConstraint("AK_UserEntityPermissions_PublicId", x => x.PublicId);
                    table.UniqueConstraint("AK_UserEntityPermissions_UserId_EntityId_PermissionId", x => new { x.UserId, x.EntityId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_UserEntityPermissions_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEntityPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEntityPermissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserEntityPermissions_EntityId",
                table: "UserEntityPermissions",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEntityPermissions_PermissionId",
                table: "UserEntityPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEntityPermissions_UserId",
                table: "UserEntityPermissions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserEntityPermissions");
        }
    }
}
