using Microsoft.EntityFrameworkCore.Migrations;

namespace Popcorn.Data.Migrations
{
    public partial class ChangeSaloonForeginKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Saloons_Cinemas_Cinemaid",
                table: "Saloons");

            migrationBuilder.DropIndex(
                name: "IX_Saloons_Cinemaid",
                table: "Saloons");

            migrationBuilder.RenameColumn(
                name: "Cinemaid",
                table: "Saloons",
                newName: "CinemaId");

            migrationBuilder.AddColumn<int>(
                name: "CinemaModelid",
                table: "Saloons",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Saloons_CinemaModelid",
                table: "Saloons",
                column: "CinemaModelid");

            migrationBuilder.AddForeignKey(
                name: "FK_Saloons_Cinemas_CinemaModelid",
                table: "Saloons",
                column: "CinemaModelid",
                principalTable: "Cinemas",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Saloons_Cinemas_CinemaModelid",
                table: "Saloons");

            migrationBuilder.DropIndex(
                name: "IX_Saloons_CinemaModelid",
                table: "Saloons");

            migrationBuilder.DropColumn(
                name: "CinemaModelid",
                table: "Saloons");

            migrationBuilder.RenameColumn(
                name: "CinemaId",
                table: "Saloons",
                newName: "Cinemaid");

            migrationBuilder.CreateIndex(
                name: "IX_Saloons_Cinemaid",
                table: "Saloons",
                column: "Cinemaid");

            migrationBuilder.AddForeignKey(
                name: "FK_Saloons_Cinemas_Cinemaid",
                table: "Saloons",
                column: "Cinemaid",
                principalTable: "Cinemas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
