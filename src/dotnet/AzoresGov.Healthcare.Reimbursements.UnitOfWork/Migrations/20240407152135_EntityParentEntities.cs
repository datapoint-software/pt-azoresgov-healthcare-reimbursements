using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class EntityParentEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntityParentEntities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PublicId = table.Column<Guid>(type: "char(36)", nullable: false),
                    RowVersionId = table.Column<Guid>(type: "char(36)", nullable: false),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    ParentEntityId = table.Column<long>(type: "bigint", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityParentEntities", x => x.Id);
                    table.UniqueConstraint("AK_EntityParentEntities_PublicId", x => x.PublicId);
                    table.ForeignKey(
                        name: "FK_EntityParentEntities_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityParentEntities_Entities_ParentEntityId",
                        column: x => x.ParentEntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EntityParentEntities_EntityId_Level",
                table: "EntityParentEntities",
                columns: new[] { "EntityId", "Level" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityParentEntities_EntityId_ParentEntityId",
                table: "EntityParentEntities",
                columns: new[] { "EntityId", "ParentEntityId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityParentEntities_ParentEntityId",
                table: "EntityParentEntities",
                column: "ParentEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityParentEntities");
        }
    }
}
