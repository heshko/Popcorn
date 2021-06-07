using Microsoft.EntityFrameworkCore.Migrations;

namespace Popcorn.Data.Migrations
{
    public partial class AddCinematoSaloon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_Saloons_CinemaId",
                table: "Saloons",
                column: "CinemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Saloons_Cinemas_CinemaId",
                table: "Saloons",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Saloons_Cinemas_CinemaId",
                table: "Saloons");

            migrationBuilder.DropIndex(
                name: "IX_Saloons_CinemaId",
                table: "Saloons");

            migrationBuilder.AddColumn<int>(
                name: "CinemaModelid",
                table: "Saloons",
                type: "int",
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
    }
}
