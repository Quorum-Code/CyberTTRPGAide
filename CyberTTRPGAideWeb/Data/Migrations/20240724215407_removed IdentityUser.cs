using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberTTRPGAideWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class removedIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterSheet_AspNetUsers_IdentityUserId",
                table: "CharacterSheet");

            migrationBuilder.DropIndex(
                name: "IX_CharacterSheet_IdentityUserId",
                table: "CharacterSheet");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "CharacterSheet");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "CharacterSheet",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSheet_IdentityUserId",
                table: "CharacterSheet",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterSheet_AspNetUsers_IdentityUserId",
                table: "CharacterSheet",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
