using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberTTRPGAideWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class itemEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Characters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ItemEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemEntry_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemEntry_GameItem_ItemId",
                        column: x => x.ItemId,
                        principalTable: "GameItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemEntry_CharacterId",
                table: "ItemEntry",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemEntry_ItemId",
                table: "ItemEntry",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemEntry");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Characters");
        }
    }
}
