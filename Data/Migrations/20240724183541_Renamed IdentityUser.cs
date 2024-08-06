using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberTTRPGAideWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamedIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterSheet_AspNetUsers_UserId",
                table: "CharacterSheet");

            migrationBuilder.DropIndex(
                name: "IX_CharacterSheet_UserId",
                table: "CharacterSheet");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CharacterSheet",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CharacterSheet",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSheet_UserId",
                table: "CharacterSheet",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterSheet_AspNetUsers_UserId",
                table: "CharacterSheet",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
